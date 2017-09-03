using ABClient.Helpers;
using System;
using System.Text;
using ABClient.ABForms;
using ABClient.MyHelpers;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static string MainPhpFast(string html)
        {
            switch (AppVars.FastId)
            {
                case "i_w28_22.gif": // Свиток телепорта
                    return MainPhpFastTeleport(html);
                case "i_w28_23.gif": // Свиток саморассеивания
                    return MainPhpFastSelfRass(html);
                case "i_w28_28.gif": // Свиток обнаружения
                    return MainPhpFastOpenNevid(html);
                case "i_svi_213.gif": // Свиток искажающего тумана
                    return MainPhpFastFog(html);
                case "i_svi_001.gif": // Обычная нападалка
                    return MainPhpFastHit(html);
                case "i_svi_002.gif": // Кровавая нападалка
                    return MainPhpFastBloodHit(html);
                case "i_w28_26.gif": // Боевая нападалка
                    return MainPhpFastUltimateHit(html);
                case "i_w28_26X.gif": // Закрытая боевая нападалка
                    return MainPhpFastClosedUltimateHit(html);
                case "i_w28_24.gif": // Кулачка
                    return MainPhpFastFist(html);
                case "i_w28_25.gif": // Закрытая кулачка
                    return MainPhpFastClosedFist(html);
                case "i_svi_205.gif": // Закрытая нападалка
                    return MainPhpFastClosedHit(html);
                case "i_w28_27.gif": // Свиток защиты
                    return MainPhpFastZas(html);
                case "Телепорт (Остров Туротор)":
                    return MainPhpFastIsland(html);
                case "i_w28_86.gif":
                    return MainPhpFastPortal(html);
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
                    return MainPhpFastPotion(html);
                case "Эликсир Блаженства":
                case "Эликсир Мгновенного Исцеления":
                case "Эликсир Восстановления":
                case AppConsts.Bait:
                    return MainPhpFastElixir(html);
                case "Тотем":
                    return MainPhpFastTotem(html);
                default:
                    throw new NotImplementedException($"AppVars.FastId = {AppVars.FastId}");
            }
        }

        private static string MainPhpFastTeleport(string html)
        {
            /*
                onclick="w28_form('74549bbe6688b515f8ac796b39d47035','72451012',22,'1')" 
             * 
                 case 22: 
             *      wadd = 1; 
             *      vtitle = 'Применить телепорт?'; 
             *      vcont = '<INPUT type=hidden name=post_id value=25>
             *      <INPUT type=hidden name=vcode value='+vcode+'>
             *      <INPUT type=hidden name=wuid value='+wuid+'>
             *      <INPUT type=hidden name=wsubid value='+wsubid+'>
             *      <INPUT type=hidden name=wsolid value='+wsolid+'>
             *      <DIV align=center><FONT class=nickname><B>Пункт назначения: </B>
             *      <SELECT name=wtelid class=LogintextBox6>
             *      <OPTION VALUE=1>Город Форпост</OPTION>
             *      <OPTION VALUE=2>Город Октал</OPTION>
             *      <OPTION VALUE=3>Деревня Подгорная</OPTION>
             *      <OPTION VALUE=4>Окрестность Фейдана, Телепорт</OPTION>
             *      <OPTION VALUE=5>Окрестность Октала, Телепорт</OPTION>
             *      <OPTION VALUE=6>Окрестности Эринграда, Телепорт</OPTION>
             *      <OPTION VALUE=7>Окрестность Форпоста, Телепорт</OPTION>
             *      <OPTION VALUE=8>Пустыня Самум-Бейт, Телепорт</OPTION>
             *      <OPTION VALUE=9>Северский Тракт, Телепорт</OPTION>
             *      <OPTION VALUE=10>Восточные Леса, Телепорт</OPTION>
             *      <OPTION VALUE=11>Окрестности Кенджии, Телепорт</OPTION>
             *      <OPTION VALUE=12>Ущелье Эль-Тэр, Телепорт</OPTION></SELECT>
             *      <INPUT type=submit value="Применить" class=lbut name=agree>              
             */

            const string patternW28Form = "w28_form(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternW28Form, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternW28Form.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 4)
                    continue;

                var vcode = arg[0].Trim(new[] { '\'' });
                var wuid = arg[1].Trim(new[] { '\'' });
                var wsubid = arg[2].Trim(new[] { '\'' });
                var wsolid = arg[3].Trim(new[] { '\'' });

                if (!wsubid.Equals("22"))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Используем телепорт");
                sb.Append(AppVars.FastNick);
                sb.Append("...");
                sb.Append("<form action=main.php method=POST name=ff>");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(25);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=wuid type=hidden value=""");
                sb.Append(wuid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsubid type=hidden value=""");
                sb.Append(wsubid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsolid type=hidden value=""");
                sb.Append(wsolid);
                sb.Append(@""">");

                int wtelid = Dice.Make(12) + 1;

                sb.Append(@"<input name=wtelid type=hidden value=""");
                sb.Append(wtelid);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            return null;
        }

        private static string MainPhpFastPotion(string html)
        {
           /*
            * magicreform('47562250','Умник','Яд','15a531759e0b873bf626ee83e55cbbca')            
            */

            var namepotion = '\'' + AppVars.FastId + '\'';
            var p0 = html.IndexOf(namepotion, StringComparison.CurrentCultureIgnoreCase);
            if (p0 == -1) goto failed;
            var ps = html.LastIndexOf('<', p0);
            if (ps == -1) goto failed;
            ps++;
            var pe = html.IndexOf('>', p0);
            if (pe == -1) goto failed;
            var args = html.Substring(ps, pe - ps);
            if (args.IndexOf("magicreform(", StringComparison.CurrentCultureIgnoreCase) == -1)
            {
                goto failed;
            }

            args = HelperStrings.SubString(args, "magicreform('", "')");
            if (string.IsNullOrEmpty(args)) goto failed;
            var arg = args.Split('\'');
            if (arg.Length < 7) goto failed;
            var wuid = arg[0];
            var wmcode = arg[6];

            var sb = new StringBuilder(
                HelperErrors.Head() +
                "Используем ");
            sb.Append(AppVars.FastId);
            sb.Append("...");
            sb.Append("<form action=main.php method=POST name=ff>");

            sb.Append(@"<input name=magicrestart type=hidden value=""");
            sb.Append(1);
            sb.Append(@""">");

            sb.Append(@"<input name=magicreuid type=hidden value=""");
            sb.Append(wuid);
            sb.Append(@""">");

            sb.Append(@"<input name=vcode type=hidden value=""");
            sb.Append(wmcode);
            sb.Append(@""">");

            sb.Append(@"<input name=post_id type=hidden value=""");
            sb.Append(46);
            sb.Append(@""">");

            sb.Append(@"<input name=fornickname type=hidden value=""");
            sb.Append(AppVars.FastNick);
            sb.Append(@""">");

            sb.Append(
                @"</form>" +
                @"<script language=""JavaScript"">" +
                @"document.ff.submit();" +
                @"</script></body></html>");

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateTraceDrinkPotionDelegate(AppVars.MainForm.TraceDrinkPotion), AppVars.FastNick, AppVars.FastId);
                }
            }
            catch (InvalidOperationException)
            {
            }

            return sb.ToString();

        failed:
            return null;
        }

        private static string MainPhpFastFog(string html)
        {
            /*
             * abil_svitok('72114828','17','10','Свиток Искажающего Тумана','9e86c60bc550cd734f36458fce4f44ab')            
             */

            /*
             * abil_svitok(wuid,wmid,wmsolid,wnametxt,wmcode)           
             */

            var namesvitok = '\'' + "Свиток Искажающего Тумана" + '\'';
            var p0 = html.IndexOf(namesvitok, StringComparison.OrdinalIgnoreCase);
            if (p0 == -1) goto failed;
            var ps = html.LastIndexOf('<', p0);
            if (ps == -1) goto failed;
            ps++;
            var pe = html.IndexOf('>', p0);
            if (pe == -1) goto failed;
            var args = html.Substring(ps, pe - ps);
            if (args.IndexOf("abil_svitok(", StringComparison.Ordinal) == -1)
            {
                goto failed;
            }

            args = HelperStrings.SubString(args, "abil_svitok('", "')");
            if (string.IsNullOrEmpty(args)) goto failed;
            var arg = args.Split('\'');
            if (arg.Length < 9) goto failed;
            var wuid = arg[0];
            var wmid = arg[2];
            var wmsolid = arg[4];
            var wmcode = arg[8];

            var sb = new StringBuilder(
                HelperErrors.Head() +
                "Используем ");
            sb.Append("Свиток Искажающего Тумана");
            sb.Append("...");
            sb.Append("<form action=main.php method=POST name=ff>");

            sb.Append(@"<input name=post_id type=hidden value=""");
            sb.Append(44);
            sb.Append(@""">");

            sb.Append(@"<input name=uid type=hidden value=""");
            sb.Append(wuid);
            sb.Append(@""">");

            sb.Append(@"<input name=mid type=hidden value=""");
            sb.Append(wmid);
            sb.Append(@""">");

            sb.Append(@"<input name=curs type=hidden value=""");
            sb.Append(wmsolid);
            sb.Append(@""">");

            sb.Append(@"<input name=vcode type=hidden value=""");
            sb.Append(wmcode);
            sb.Append(@""">");

            sb.Append(@"<input name=fnick type=hidden value=""");
            sb.Append(AppVars.FastNick);
            sb.Append(@""">");

            sb.Append(
                @"</form>" +
                @"<script language=""JavaScript"">" +
                @"document.ff.submit();" +
                @"</script></body></html>");

            return sb.ToString();

        failed:
            return null;
        }

        private static string MainPhpFastZas(string html)
        {
            /*
                onclick="w28_form('c577b975f5e68633d0dd29f9e64a8677','46746724',27,'1')" 
             * 
       	      case 27: 
             * wadd = 2; 
             * vtitle = 'Свиток Защиты'; 
             * vcont = '
             * <INPUT type=hidden name=post_id value=25>
             * <INPUT type=hidden name=vcode value='+vcode+'>
             * <INPUT type=hidden name=wuid value='+wuid+'>
             * <INPUT type=hidden name=wsubid value='+wsubid+'>
             * <INPUT type=hidden name=wsolid value='+wsolid+'>
             * <DIV align=center><FONT class=nickname><B>Союзник: </B><INPUT TYPE="text" name=pnick class=LogintextBox maxlength=20> 
             * <INPUT type=submit value="Помочь" class=lbut name=agree>
             * <INPUT type=button class=lbut onclick="c_form()" value="Отмена"></DIV>'; break;
             */

            const string patternW28Form = "w28_form(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternW28Form, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternW28Form.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 4)
                    continue;

                var vcode = arg[0].Trim(new[] { '\'' });
                var wuid = arg[1].Trim(new[] { '\'' });
                var wsubid = arg[2].Trim(new[] { '\'' });
                var wsolid = arg[3].Trim(new[] { '\'' });

                if (!wsubid.Equals("27"))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Применяем свиток защиты к ");
                sb.Append(AppVars.FastNick);
                sb.Append("...");
                sb.Append("<form action=main.php method=POST name=ff>");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(25);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=wuid type=hidden value=""");
                sb.Append(wuid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsubid type=hidden value=""");
                sb.Append(wsubid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsolid type=hidden value=""");
                sb.Append(wsolid);
                sb.Append(@""">");

                sb.Append(@"<input name=pnick type=hidden value=""");
                sb.Append(AppVars.FastNick);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            return null;
        }

        private static string MainPhpFastIsland(string html)
        {
            const string str = "Использовать Свиток Телепорта сейчас?";
            var startIndex1 = html.IndexOf(str, StringComparison.CurrentCultureIgnoreCase);
            if (startIndex1 != -1)
            {
                var num1 = html.IndexOf("='", startIndex1, StringComparison.Ordinal);
                if (num1 != -1)
                {
                    var startIndex2 = num1 + 2;
                    var num2 = html.IndexOf("'", startIndex2, StringComparison.Ordinal);
                    if (num2 != -1)
                    {
                        var link = html.Substring(startIndex2, num2 - startIndex2);
                        return BuildRedirect($"Используем {AppVars.FastId}...", link);
                    }
                }
            }

            return null;
        }

        private static string MainPhpFastPortal(string html)
        {
            var startIndex = 0;
            while (startIndex != -1)
            {
                var num1 = html.IndexOf("w28_form(", startIndex, StringComparison.OrdinalIgnoreCase);
                if (num1 != -1)
                {
                    startIndex = num1 + "w28_form(".Length;
                    var num2 = html.IndexOf(")", startIndex, StringComparison.OrdinalIgnoreCase);
                    if (num2 != -1)
                    {
                        var str1 = html.Substring(startIndex, num2 - startIndex);
                        if (!string.IsNullOrEmpty(str1))
                        {
                            var strArray = str1.Split(',');
                            if (strArray.Length >= 4)
                            {
                                var str2 = strArray[0].Trim('\'');
                                var str3 = strArray[1].Trim('\'');
                                var str4 = strArray[2].Trim('\'');
                                var str5 = strArray[3].Trim('\'');
                                if (str4.Equals("86"))
                                {
                                    var stringBuilder = new StringBuilder();
                                    var str6 = HelperErrors.Head() + "Применяем портал на ";
                                    stringBuilder.Append(str6);
                                    var fastNick1 = AppVars.FastNick;
                                    stringBuilder.Append(fastNick1);
                                    var str7 = "...";
                                    stringBuilder.Append(str7);
                                    var str8 = "<form action=main.php method=POST name=ff>";
                                    stringBuilder.Append(str8);
                                    var str9 = "<input name=post_id type=hidden value=\"";
                                    stringBuilder.Append(str9);
                                    int num3 = 25;
                                    stringBuilder.Append(num3);
                                    var str10 = "\">";
                                    stringBuilder.Append(str10);
                                    var str11 = "<input name=vcode type=hidden value=\"";
                                    stringBuilder.Append(str11);
                                    var str12 = str2;
                                    stringBuilder.Append(str12);
                                    var str13 = "\">";
                                    stringBuilder.Append(str13);
                                    var str14 = "<input name=wuid type=hidden value=\"";
                                    stringBuilder.Append(str14);
                                    var str15 = str3;
                                    stringBuilder.Append(str15);
                                    var str16 = "\">";
                                    stringBuilder.Append(str16);
                                    var str17 = "<input name=wsubid type=hidden value=\"";
                                    stringBuilder.Append(str17);
                                    var str18 = str4;
                                    stringBuilder.Append(str18);
                                    var str19 = "\">";
                                    stringBuilder.Append(str19);
                                    var str20 = "<input name=wsolid type=hidden value=\"";
                                    stringBuilder.Append(str20);
                                    var str21 = str5;
                                    stringBuilder.Append(str21);
                                    var str22 = "\">";
                                    stringBuilder.Append(str22);
                                    var str23 = "<input name=pnick type=hidden value=\"";
                                    stringBuilder.Append(str23);
                                    var fastNick2 = AppVars.FastNick;
                                    stringBuilder.Append(fastNick2);
                                    var str24 = "\">";
                                    stringBuilder.Append(str24);
                                    var str25 = "</form><script language=\"JavaScript\">document.ff.submit();</script></body></html>";
                                    stringBuilder.Append(str25);
                                    return stringBuilder.ToString();
                                }
                            }
                        }
                    }
                }
                else
                    break;
            }

            return null;
        }

        private static string MainPhpFastHit(string html)
        {
            /*
                <input type=button class=invbut onclick="w28_form('c43190e45731bcb7d1c9d6838858f7f8','21723358',3,'2')" value="Использовать">
             * 
                case 4:  wadd = 2; 
             *      vtitle = 'Обычное нападение'; 
             *      vcont = '
             *      <INPUT type=hidden name=post_id value=8>
             *      <INPUT type=hidden name=vcode value='+vcode+'>
             *      <INPUT type=hidden name=wuid value='+wuid+'>
             *      <INPUT type=hidden name=wsubid value='+wsubid+'>
             *      <INPUT type=hidden name=wsolid value='+wsolid+'>
             *      <DIV align=center><FONT class=nickname><B>На кого: </B>
             *      <INPUT TYPE="text" name=pnick class=LogintextBox maxlength=20> 
             *      <INPUT type=submit value="Выполнить" class=lbut name=agree> 
             *      <INPUT type=button class=lbut onclick="c_form()" value="Отмена"></DIV>'; break;
             */

            const string patternW28Form = "w28_form(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternW28Form, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternW28Form.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 4)
                    continue;

                var vcode = arg[0].Trim(new[] { '\'' });
                var wuid = arg[1].Trim(new[] { '\'' });
                var wsubid = arg[2].Trim(new[] { '\'' });
                var wsolid = arg[3].Trim(new[] { '\'' });

                if (!wsubid.Equals("1") && !wsubid.Equals("2") && !wsubid.Equals("3") && !wsubid.Equals("4"))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Используем обычную нападалку на ");
                sb.Append(AppVars.FastNick);
                sb.Append("...");
                sb.Append("<form action=main.php method=POST name=ff>");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(8);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=wuid type=hidden value=""");
                sb.Append(wuid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsubid type=hidden value=""");
                sb.Append(wsubid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsolid type=hidden value=""");
                sb.Append(wsolid);
                sb.Append(@""">");

                sb.Append(@"<input name=pnick type=hidden value=""");
                sb.Append(AppVars.FastNick);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            TellItemNotFound("Нападалка не найдена");
            return null;
        }

        private static string MainPhpFastBloodHit(string html)
        {
            /*
             *   <input type=button class=invbut onclick="w28_form('58c90c2062ed17abb5b3787165155aad','32423321',8,'2')"
             * 
	      case 5:
	      case 6:
	      case 7:
	      case 8:  wadd = 2; 
             * vtitle = 'Кровавое нападение'; 
             * vcont = '
             * <INPUT type=hidden name=post_id value=8>
             * <INPUT type=hidden name=vcode value='+vcode+'>
             * <INPUT type=hidden name=wuid value='+wuid+'>
             * <INPUT type=hidden name=wsubid value='+wsubid+'>
             * <INPUT type=hidden name=wsolid value='+wsolid+'>
             * <DIV align=center>
             * <FONT class=nickname><B>На кого: </B>
             * <INPUT TYPE="text" name=pnick class=LogintextBox maxlength=20>
             * <INPUT type=submit value="Выполнить" class=lbut name=agree>
             * <INPUT type=button class=lbut onclick="c_form()" value="Отмена"></DIV>'; break;
             */

            const string patternW28Form = "w28_form(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternW28Form, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternW28Form.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 4)
                    continue;

                var vcode = arg[0].Trim(new[] { '\'' });
                var wuid = arg[1].Trim(new[] { '\'' });
                var wsubid = arg[2].Trim(new[] { '\'' });
                var wsolid = arg[3].Trim(new[] { '\'' });

                if (!wsubid.Equals("5") && !wsubid.Equals("6") && !wsubid.Equals("7") && !wsubid.Equals("8"))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Используем кровавую нападалку на ");
                sb.Append(AppVars.FastNick);
                sb.Append("...");
                sb.Append("<form action=main.php method=POST name=ff>");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(8);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=wuid type=hidden value=""");
                sb.Append(wuid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsubid type=hidden value=""");
                sb.Append(wsubid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsolid type=hidden value=""");
                sb.Append(wsolid);
                sb.Append(@""">");

                sb.Append(@"<input name=pnick type=hidden value=""");
                sb.Append(AppVars.FastNick);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            TellItemNotFound("Кровавая нападалка не найдена");
            return null;
        }

        private static string MainPhpFastUltimateHit(string html)
        {
            /*
             * <input type=button class=invbut onclick="w28_form('c455e85cb251f5e98d11c6597e1d53df','72226641',26,'784')" value="Использовать">
             *
       	      case 26: 
             * wadd = 2; 
             * vtitle = 'Боевое нападение'; 
             * vcont = '
             * <INPUT type=hidden name=post_id value=8><INPUT type=hidden name=vcode value='+vcode+'>
             * <INPUT type=hidden name=wuid value='+wuid+'>
             * <INPUT type=hidden name=wsubid value='+wsubid+'>
             * <INPUT type=hidden name=wsolid value='+wsolid+'>
             * <DIV align=center><FONT class=nickname><B>На кого: </B>
             * <INPUT TYPE="text" name=pnick class=LogintextBox maxlength=20> <INPUT type=submit value="Выполнить" class=lbut name=agree>
             */

            const string patternW28Form = "w28_form(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternW28Form, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternW28Form.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 4)
                    continue;

                var vcode = arg[0].Trim(new[] { '\'' });
                var wuid = arg[1].Trim(new[] { '\'' });
                var wsubid = arg[2].Trim(new[] { '\'' });
                var wsolid = arg[3].Trim(new[] { '\'' });

                if (!wsubid.Equals("26"))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Используем боевую нападалку на ");
                sb.Append(AppVars.FastNick);
                sb.Append("...");
                sb.Append("<form action=main.php method=POST name=ff>");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(8);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=wuid type=hidden value=""");
                sb.Append(wuid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsubid type=hidden value=""");
                sb.Append(wsubid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsolid type=hidden value=""");
                sb.Append(wsolid);
                sb.Append(@""">");

                sb.Append(@"<input name=pnick type=hidden value=""");
                sb.Append(AppVars.FastNick);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            TellItemNotFound("Боевая нападалка не найдена");
            return null;
        }

        private static string MainPhpFastClosedUltimateHit(string html)
        {
            /*
             * <input type=button class=invbut onclick="w28_form('13a2d143ad2fe9ca4fce2d1ca41268c3','47638001',29,'9')" value="Использовать">
             *
              case 29: 
             * wadd = 2; 
             * vtitle = 'Закрытое боевое нападение?'; 
             * vcont = '
             * <INPUT type=hidden name=post_id value=8><INPUT type=hidden name=vcode value='+vcode+'>
             * <INPUT type=hidden name=wuid value='+wuid+'>
             * <INPUT type=hidden name=wsubid value='+wsubid+'>
             * <INPUT type=hidden name=wsolid value='+wsolid+'>
             * <DIV align=center><FONT class=nickname><B>На кого: </B>
             * <INPUT TYPE="text" name=pnick class=LogintextBox maxlength=20>
             * <INPUT type=submit value="Выполнить" class=lbut name=agree>
             */

            const string patternW28Form = "w28_form(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternW28Form, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternW28Form.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 4)
                    continue;

                var vcode = arg[0].Trim(new[] { '\'' });
                var wuid = arg[1].Trim(new[] { '\'' });
                var wsubid = arg[2].Trim(new[] { '\'' });
                var wsolid = arg[3].Trim(new[] { '\'' });

                if (!wsubid.Equals("29"))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Используем закрытую боевую нападалку на ");
                sb.Append(AppVars.FastNick);
                sb.Append("...");
                sb.Append("<form action=main.php method=POST name=ff>");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(8);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=wuid type=hidden value=""");
                sb.Append(wuid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsubid type=hidden value=""");
                sb.Append(wsubid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsolid type=hidden value=""");
                sb.Append(wsolid);
                sb.Append(@""">");

                sb.Append(@"<input name=pnick type=hidden value=""");
                sb.Append(AppVars.FastNick);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            TellItemNotFound("Закрытая боевая нападалка не найдена");
            return null;
        }

        private static string MainPhpFastFist(string html)
        {
            /*
             * <input type=button class=invbut onclick="w28_form('f5879b7631a2eea2b37730b9f1609dd3','46461028',24,'1')" value="Использовать">
             *
       	      case 24: 
             * wadd = 2; 
             * vtitle = 'Кулачное нападение'; vcont = '
             * <INPUT type=hidden name=post_id value=8>
             * <INPUT type=hidden name=vcode value='+vcode+'>
             * <INPUT type=hidden name=wuid value='+wuid+'>
             * <INPUT type=hidden name=wsubid value='+wsubid+'>
             * <INPUT type=hidden name=wsolid value='+wsolid+'>
             * <DIV align=center><FONT class=nickname><B>На кого: </B>
             * <INPUT TYPE="text" name=pnick class=LogintextBox maxlength=20>
             */

            const string patternW28Form = "w28_form(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternW28Form, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternW28Form.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 4)
                    continue;

                var vcode = arg[0].Trim(new[] { '\'' });
                var wuid = arg[1].Trim(new[] { '\'' });
                var wsubid = arg[2].Trim(new[] { '\'' });
                var wsolid = arg[3].Trim(new[] { '\'' });

                if (!wsubid.Equals("24"))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Используем кулачку на ");
                sb.Append(AppVars.FastNick);
                sb.Append("...");
                sb.Append("<form action=main.php method=POST name=ff>");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(8);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=wuid type=hidden value=""");
                sb.Append(wuid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsubid type=hidden value=""");
                sb.Append(wsubid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsolid type=hidden value=""");
                sb.Append(wsolid);
                sb.Append(@""">");

                sb.Append(@"<input name=pnick type=hidden value=""");
                sb.Append(AppVars.FastNick);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            TellItemNotFound("Кулачка не найдена");
            return null;
        }

        private static string MainPhpFastClosedFist(string html)
        {
            /*
             * <input type=button class=invbut onclick="w28_form('bd30fdd37a33e47c9efa68314d0eba3d','46882574',25,'1')" value="Использовать">
             *
       	      case 25: 
             * wadd = 2; 
             * vtitle = 'Закрытое кулачное нападение'; 
             * vcont = '<INPUT type=hidden name=post_id value=8>
             * <INPUT type=hidden name=vcode value='+vcode+'>
             * <INPUT type=hidden name=wuid value='+wuid+'>
             * <INPUT type=hidden name=wsubid value='+wsubid+'>
             * <INPUT type=hidden name=wsolid value='+wsolid+'>
             * <DIV align=center><FONT class=nickname><B>На кого: </B>
             * <INPUT TYPE="text" name=pnick class=LogintextBox maxlength=20>
             * <INPUT type=submit value="Выполнить" class=lbut name=agree>
             */

            const string patternW28Form = "w28_form(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternW28Form, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternW28Form.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 4)
                    continue;

                var vcode = arg[0].Trim(new[] { '\'' });
                var wuid = arg[1].Trim(new[] { '\'' });
                var wsubid = arg[2].Trim(new[] { '\'' });
                var wsolid = arg[3].Trim(new[] { '\'' });

                if (!wsubid.Equals("25"))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Используем закрытую кулачку на ");
                sb.Append(AppVars.FastNick);
                sb.Append("...");
                sb.Append("<form action=main.php method=POST name=ff>");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(8);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=wuid type=hidden value=""");
                sb.Append(wuid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsubid type=hidden value=""");
                sb.Append(wsubid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsolid type=hidden value=""");
                sb.Append(wsolid);
                sb.Append(@""">");

                sb.Append(@"<input name=pnick type=hidden value=""");
                sb.Append(AppVars.FastNick);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            TellItemNotFound("Закрытая кулачка не найдена");
            return null;
        }

        private static string MainPhpFastSelfRass(string html)
        {
            /* internal void 
             * onclick="w28_form('428ff4aab6924c76a183387d059a8f22','72882173',23,'1')"
             *
             * case 23: 
             * wadd = 1; 
             * vtitle = 'Рассеять невидимость?'
             * vcont = '
             * <INPUT type=hidden name=post_id value=25>
             * <INPUT type=hidden name=vcode value='+vcode+'>
             * <INPUT type=hidden name=wuid value='+wuid+'>
             * <INPUT type=hidden name=wsubid value='+wsubid+'>
             * <INPUT type=hidden name=wsolid value='+wsolid+'>
             */

            const string patternW28Form = "w28_form(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternW28Form, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternW28Form.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 4)
                    continue;

                var vcode = arg[0].Trim(new[] { '\'' });
                var wuid = arg[1].Trim(new[] { '\'' });
                var wsubid = arg[2].Trim(new[] { '\'' });
                var wsolid = arg[3].Trim(new[] { '\'' });

                if (!wsubid.Equals("23"))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Применяем свиток рассеивания невидимости на себя...");
                sb.Append("<form action=main.php method=POST name=ff>");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(25);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=wuid type=hidden value=""");
                sb.Append(wuid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsubid type=hidden value=""");
                sb.Append(wsubid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsolid type=hidden value=""");
                sb.Append(wsolid);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            TellItemNotFound("Свиток рассеивания невидимости не найден");
            return null;
        }


        private static string MainPhpFastOpenNevid(string html)
        {
            /* internal void FastAttackOpenNevid()
             * <input type=button class=invbut onclick="w28_form('90e74a29c879926d22bd9fc89a4904e0','73493740',28,'66')" 
             *
             * case 28: 
             * wadd = 1; 
             * vtitle = 'Применить Свиток Обнаружения?'; 
             * vcont = '
             * <INPUT type=hidden name=post_id value=25>
             * <INPUT type=hidden name=vcode value='+vcode+'>
             * <INPUT type=hidden name=wuid value='+wuid+'>
             * <INPUT type=hidden name=wsubid value='+wsubid+'>
             * <INPUT type=hidden name=wsolid value='+wsolid+'>
             */

            const string patternW28Form = "w28_form(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternW28Form, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternW28Form.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 4)
                    continue;

                var vcode = arg[0].Trim(new[] { '\'' });
                var wuid = arg[1].Trim(new[] { '\'' });
                var wsubid = arg[2].Trim(new[] { '\'' });
                var wsolid = arg[3].Trim(new[] { '\'' });

                if (!wsubid.Equals("28"))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Применяем свиток обнаружения...");
                sb.Append("<form action=main.php method=POST name=ff>");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(25);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=wuid type=hidden value=""");
                sb.Append(wuid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsubid type=hidden value=""");
                sb.Append(wsubid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsolid type=hidden value=""");
                sb.Append(wsolid);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            TellItemNotFound("Свиток обнаружения не найден");
            return null;
        }

        private static string MainPhpFastClosedHit(string html)
        {
            /*
             * <input type=button class=invbut onclick="w28_form('852bcccb4781a9bb67455a8e3b6f34bf','75998530',14,'1')" value="Использовать">
             *
	         * case 14: 
             * wadd = 2; 
             * vtitle = 'Закрытое нападение'; 
             * <INPUT type=hidden name=post_id value=8>
             * <INPUT type=hidden name=vcode value='+vcode+'>
             * <INPUT type=hidden name=wuid value='+wuid+'>
             * <INPUT type=hidden name=wsubid value='+wsubid+'>
             * <INPUT type=hidden name=wsolid value='+wsolid+'>
             * <INPUT TYPE="text" name=pnick class=LogintextBox maxlength=20>
             */

            const string patternW28Form = "w28_form(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternW28Form, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternW28Form.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 4)
                    continue;

                var vcode = arg[0].Trim(new[] { '\'' });
                var wuid = arg[1].Trim(new[] { '\'' });
                var wsubid = arg[2].Trim(new[] { '\'' });
                var wsolid = arg[3].Trim(new[] { '\'' });

                if (!wsubid.Equals("14"))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Используем закрытое нападение на ");
                sb.Append(AppVars.FastNick);
                sb.Append("...");
                sb.Append("<form action=main.php method=POST name=ff>");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(8);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=wuid type=hidden value=""");
                sb.Append(wuid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsubid type=hidden value=""");
                sb.Append(wsubid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsolid type=hidden value=""");
                sb.Append(wsolid);
                sb.Append(@""">");

                sb.Append(@"<input name=pnick type=hidden value=""");
                sb.Append(AppVars.FastNick);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            TellItemNotFound("Закрытое нападение не найдено");
            return null;
        }

        private static string MainPhpFastElixir(string html)
        {
            // confirm('Использовать Эликсир Блаженства сейчас?')) { location='main.php?get_id=43&act=107&uid=46612489&curs=10&vcode=c93e416ed4cfa70c373669aee2eda9c8'            

            var namepotion = string.Format("Использовать {0} сейчас?", AppVars.FastId);
            var p0 = html.IndexOf(namepotion, StringComparison.CurrentCultureIgnoreCase);
            if (p0 == -1) goto failed;
            var ps = html.IndexOf("='", p0, StringComparison.Ordinal);
            if (ps == -1) goto failed;
            ps += 2;
            var pe = html.IndexOf("'", ps, StringComparison.Ordinal);
            if (pe == -1) goto failed;
            var link = html.Substring(ps, pe - ps);
            var description = string.Format("Используем {0}...", AppVars.FastId);
            var htmlElixir = BuildRedirect(description, link);
            return htmlElixir;
        failed:
            return null;            
        }

        private static void TellItemNotFound(string message)
        {
            if (AppVars.MainForm != null)
                AppVars.MainForm.WriteChatMsgSafe(string.Format("<font color=#FF0000>{0} в инвентаре, действие отменено</font>", message));
        }

        private static string MainPhpDrinkBlazPotOrElixir(string html)
        {
            /*
            * magicreform('47562250','Умник','Зелье Блаженства','15a531759e0b873bf626ee83e55cbbca')
            * confirm('Использовать Эликсир Блаженства сейчас?')) { location='main.php?get_id=43&act=107&uid=74440083&curs=11&subid=0&ft=1398937453&vcode=8c92cd43b1deb45eb76e1d898faddd5b'
             * 'Использовать Эликсир Блаженства сейчас?')) { location='main.php?get_id=43&act=107&uid=75598313&curs=3&subid=0&ft=0&vcode=ca6845c87558edba77f9e811e913a474' }"
            */

            int[] order = new int[2];
            order[0] = 0;
            order[1] = 1;
            if (AppVars.Profile.AutoDrinkBlazOrder == 1)
            {
                order[0] = 1;
                order[1] = 0;
            }

            int orderIndex = 0;
            while (orderIndex < 2)
            {
                switch (order[orderIndex])
                {
                    case 0:
                        var namepotion = '\'' + "Зелье Блаженства" + '\'';
                        var p0 = html.IndexOf(namepotion, StringComparison.CurrentCultureIgnoreCase);
                        if (p0 != -1)
                        {
                            var ps = html.LastIndexOf('<', p0);
                            if (ps != -1)
                            {
                                ps++;
                                var pe = html.IndexOf('>', p0);
                                if (pe != -1)
                                {
                                    var args = html.Substring(ps, pe - ps);
                                    if (args.IndexOf("magicreform(", StringComparison.CurrentCultureIgnoreCase) != -1)
                                    {
                                        args = HelperStrings.SubString(args, "magicreform('", "')");
                                        if (!string.IsNullOrEmpty(args))
                                        {
                                            var arg = args.Split('\'');
                                            if (arg.Length >= 7)
                                            {
                                                var wuid = arg[0];
                                                var wmcode = arg[6];

                                                var sb = new StringBuilder(
                                                    HelperErrors.Head() +
                                                    "Используем ");
                                                sb.Append("Зелье Блаженства");
                                                sb.Append("...");
                                                sb.Append("<form action=main.php method=POST name=ff>");

                                                sb.Append(@"<input name=magicrestart type=hidden value=""");
                                                sb.Append(1);
                                                sb.Append(@""">");

                                                sb.Append(@"<input name=magicreuid type=hidden value=""");
                                                sb.Append(wuid);
                                                sb.Append(@""">");

                                                sb.Append(@"<input name=vcode type=hidden value=""");
                                                sb.Append(wmcode);
                                                sb.Append(@""">");

                                                sb.Append(@"<input name=post_id type=hidden value=""");
                                                sb.Append(46);
                                                sb.Append(@""">");

                                                sb.Append(@"<input name=fornickname type=hidden value=""");
                                                sb.Append(AppVars.Profile.UserNick);
                                                sb.Append(@""">");

                                                sb.Append(
                                                    @"</form>" +
                                                    @"<script language=""JavaScript"">" +
                                                    @"document.ff.submit();" +
                                                    @"</script></body></html>");

                                                return sb.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        break;

                    case 1:
                        namepotion = string.Format("Использовать {0} сейчас?", "Эликсир Блаженства");
                        p0 = html.IndexOf(namepotion, StringComparison.CurrentCultureIgnoreCase);
                        if (p0 != -1)
                        {
                            var ps = html.IndexOf("='", p0, StringComparison.Ordinal);
                            if (ps != -1)
                            {
                                ps += 2;
                                var pe = html.IndexOf("'", ps, StringComparison.Ordinal);
                                if (pe != -1)
                                {
                                    var link = html.Substring(ps, pe - ps);
                                    var description = string.Format("Используем {0}...", "Эликсир Блаженства");
                                    var htmlElixir = BuildRedirect(description, link);
                                    return htmlElixir;
                                }
                            }
                        }

                        break;
                }

                orderIndex++;
            }

            return null;           
        }

        private static string MainPhpNevidPotion(string html)
        {
            /*
             * magicreform('47562250','Умник','Яд','15a531759e0b873bf626ee83e55cbbca')            
             */

            var namepotion = '\'' + "Зелье Невидимости" + '\'';
            var p0 = html.IndexOf(namepotion, StringComparison.CurrentCultureIgnoreCase);
            if (p0 == -1) goto failed;
            var ps = html.LastIndexOf('<', p0);
            if (ps == -1) goto failed;
            ps++;
            var pe = html.IndexOf('>', p0);
            if (pe == -1) goto failed;
            var args = html.Substring(ps, pe - ps);
            if (args.IndexOf("magicreform(", StringComparison.CurrentCultureIgnoreCase) == -1)
            {
                goto failed;
            }

            args = HelperStrings.SubString(args, "magicreform('", "')");
            if (string.IsNullOrEmpty(args)) goto failed;
            var arg = args.Split('\'');
            if (arg.Length < 7) goto failed;
            var wuid = arg[0];
            var wmcode = arg[6];

            var sb = new StringBuilder(
                HelperErrors.Head() +
                "Используем ");
            sb.Append("Зелье Невидимости");
            sb.Append("...");
            sb.Append("<form action=main.php method=POST name=ff>");

            sb.Append(@"<input name=magicrestart type=hidden value=""");
            sb.Append(1);
            sb.Append(@""">");

            sb.Append(@"<input name=magicreuid type=hidden value=""");
            sb.Append(wuid);
            sb.Append(@""">");

            sb.Append(@"<input name=vcode type=hidden value=""");
            sb.Append(wmcode);
            sb.Append(@""">");

            sb.Append(@"<input name=post_id type=hidden value=""");
            sb.Append(46);
            sb.Append(@""">");

            sb.Append(@"<input name=fornickname type=hidden value=""");
            sb.Append(AppVars.Profile.UserNick);
            sb.Append(@""">");

            sb.Append(
                @"</form>" +
                @"<script language=""JavaScript"">" +
                @"document.ff.submit();" +
                @"</script></body></html>");

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateTraceDrinkPotionDelegate(AppVars.MainForm.TraceDrinkPotion),
                        new object[] { AppVars.Profile.UserNick, "Зелье Невидимости" });
                }
            }
            catch (InvalidOperationException)
            {
            }

            return sb.ToString();

        failed:
            return null;
        }

        private static string MainPhpSviNevidFourHour(string html)
        {
            /*
             * onclick="w28_form('74549bbe6688b515f8ac796b39d47035','72451012',9,'1')" 
             * 
             * case 9:  
             * wadd = 1; 
             * <INPUT type=hidden name=post_id value=25>
             * <INPUT type=hidden name=vcode value='+vcode+'>
             * <INPUT type=hidden name=wuid value='+wuid+'>
             * <INPUT type=hidden name=wsubid value='+wsubid+'>
             * <INPUT type=hidden name=wsolid value='+wsolid+'>           
             */

            const string patternW28Form = "w28_form(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternW28Form, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternW28Form.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 4)
                    continue;

                var vcode = arg[0].Trim(new[] { '\'' });
                var wuid = arg[1].Trim(new[] { '\'' });
                var wsubid = arg[2].Trim(new[] { '\'' });
                var wsolid = arg[3].Trim(new[] { '\'' });

                if (!wsubid.Equals("9"))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Используем свиток невидимости на 4 часа");
                sb.Append("...");
                sb.Append("<form action=main.php method=POST name=ff>");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(25);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=wuid type=hidden value=""");
                sb.Append(wuid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsubid type=hidden value=""");
                sb.Append(wsubid);
                sb.Append(@""">");

                sb.Append(@"<input name=wsolid type=hidden value=""");
                sb.Append(wsolid);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            return null;
        }

        private static string MainPhpSelfSviFog(string html)
        {
            /*
             * abil_svitok('72114828','17','10','Свиток Искажающего Тумана','9e86c60bc550cd734f36458fce4f44ab')            
             */

            /*
             * abil_svitok(wuid,wmid,wmsolid,wnametxt,wmcode)           
             */

            var namesvitok = '\'' + "Свиток Искажающего Тумана" + '\'';
            var p0 = html.IndexOf(namesvitok, StringComparison.OrdinalIgnoreCase);
            if (p0 == -1) goto failed;
            var ps = html.LastIndexOf('<', p0);
            if (ps == -1) goto failed;
            ps++;
            var pe = html.IndexOf('>', p0);
            if (pe == -1) goto failed;
            var args = html.Substring(ps, pe - ps);
            if (args.IndexOf("abil_svitok(", StringComparison.Ordinal) == -1)
            {
                goto failed;
            }

            args = HelperStrings.SubString(args, "abil_svitok('", "')");
            if (string.IsNullOrEmpty(args)) goto failed;
            var arg = args.Split('\'');
            if (arg.Length < 9) goto failed;
            var wuid = arg[0];
            var wmid = arg[2];
            var wmsolid = arg[4];
            var wmcode = arg[8];

            var sb = new StringBuilder(
                HelperErrors.Head() +
                "Используем ");
            sb.Append("Свиток Искажающего Тумана");
            sb.Append("...");
            sb.Append("<form action=main.php method=POST name=ff>");

            sb.Append(@"<input name=post_id type=hidden value=""");
            sb.Append(44);
            sb.Append(@""">");

            sb.Append(@"<input name=uid type=hidden value=""");
            sb.Append(wuid);
            sb.Append(@""">");

            sb.Append(@"<input name=mid type=hidden value=""");
            sb.Append(wmid);
            sb.Append(@""">");

            sb.Append(@"<input name=curs type=hidden value=""");
            sb.Append(wmsolid);
            sb.Append(@""">");

            sb.Append(@"<input name=vcode type=hidden value=""");
            sb.Append(wmcode);
            sb.Append(@""">");

            sb.Append(@"<input name=fnick type=hidden value=""");
            sb.Append(AppVars.Profile.UserNick);
            sb.Append(@""">");

            sb.Append(
                @"</form>" +
                @"<script language=""JavaScript"">" +
                @"document.ff.submit();" +
                @"</script></body></html>");

            return sb.ToString();

        failed:
            return null;
        }

        private static string MainPhpFastTotem(string html)
        {
            /*
             * ["fig","Напасть","63c22d6eb8128b2b16251af2224806f3",[]
             * 
             * <form action=main.php method=POST>
             * <input type=hidden name=post_id value="8">
             * <input type=hidden name=vcode value=' + vcode + '>
             * <input type="text" name=pnick class=gr_text maxlength=20>
             */

            const string patternEnter = @"[""fig"",""Напасть"",""";
            var posPatternEnter = html.IndexOf(patternEnter, StringComparison.OrdinalIgnoreCase);
            if (posPatternEnter == -1)
            {
                goto failed;
            }

            posPatternEnter += patternEnter.Length;
            var posEnd = html.IndexOf('"', posPatternEnter);
            if (posEnd == -1)
            {
                goto failed;
            }

            var vcode = html.Substring(posPatternEnter, posEnd - posPatternEnter);
            var sb = new StringBuilder(
                HelperErrors.Head() +
                "Используем ");
            sb.Append("тотемное нападение");
            sb.Append("...");
            sb.Append("<form action=main.php method=POST name=ff>");

            sb.Append(@"<input name=post_id type=hidden value=""");
            sb.Append(8);
            sb.Append(@""">");

            sb.Append(@"<input name=vcode type=hidden value=""");
            sb.Append(vcode);
            sb.Append(@""">");

            sb.Append(@"<input name=pnick type=hidden value=""");
            sb.Append(AppVars.FastNick);
            sb.Append(@""">");

            sb.Append(
                @"</form>" +
                @"<script language=""JavaScript"">" +
                @"document.ff.submit();" +
                @"</script></body></html>");

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateTraceDrinkPotionDelegate(AppVars.MainForm.TraceDrinkPotion),
                        new object[] { AppVars.FastNick, AppVars.FastId });
                }
            }
            catch (InvalidOperationException)
            {
            }

            return sb.ToString();

        failed:
            return null;
        }
    }
}