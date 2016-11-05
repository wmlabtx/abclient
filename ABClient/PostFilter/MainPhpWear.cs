using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ABClient.MyHelpers;

namespace ABClient.PostFilter
{
    using System;
    using ABForms;

    internal static partial class Filter
    {
        private static string MainPhpWearUd(string html)
        {
            var ud = new ParsedDressed(html);
            if (!ud.Valid)
                return null;

            var invList = GetInvList(html);

            // Проверяем, одета ли первая удочка
            var iswear1 = ud.IsWear1();

            // Если нет - ищем и одеваем
            if (!iswear1 && AppVars.Profile.FishAutoWear)
            {
                foreach (var thing in invList)
                {
                    if (AppVars.Profile.FishHandOne.Equals("Любая удочка", StringComparison.OrdinalIgnoreCase))
                    {
                        if (thing.Name.IndexOf("удочка", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                            thing.Name.IndexOf("спиннинг", StringComparison.CurrentCultureIgnoreCase) >= 0)
                        {
                            if (!string.IsNullOrEmpty(thing.WearLink))
                            {
                                return BuildRedirect("Одеваем первую попавшуюся удочку", thing.WearLink);
                            }
                        }
                    }
                    else
                    {
                        if (thing.Name.IndexOf(AppVars.Profile.FishHandOne, StringComparison.CurrentCultureIgnoreCase) >= 0)
                        {
                            if (!string.IsNullOrEmpty(thing.WearLink))
                            {
                                return BuildRedirect($"{AppVars.Profile.FishHandOne} одевается", thing.WearLink);
                            }
                        }
                    }
                }

                goto stopautofish;
            }

            // Проверяем, одета ли вторая удочка
            var iswear2 = ud.IsWear2();

            // Если нет - ищем и одеваем
            if (!iswear2 && AppVars.Profile.FishAutoWear)
            {
                foreach (var thing in invList)
                {
                    if (AppVars.Profile.FishHandTwo.Equals("Любая удочка", StringComparison.OrdinalIgnoreCase))
                    {
                        if (thing.Name.IndexOf("удочка", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                            thing.Name.IndexOf("спиннинг", StringComparison.CurrentCultureIgnoreCase) >= 0)
                        {
                            if (!string.IsNullOrEmpty(thing.WearLink))
                            {
                                if ((ud.Empty1 || ud.Empty2) || !ud.InRightSlot)
                                    return BuildRedirect("Одеваем первую попавшуюся удочку", thing.WearLink);

                                return BuildRedirect("Снимаем " + ud.Hand1, "main.php?get_id=57&uid=" + ud.Wid + "&s=0&vcode=" + ud.Vcod);
                            }
                        }
                    }
                    else
                    {
                        if (thing.Name.IndexOf(AppVars.Profile.FishHandTwo, StringComparison.CurrentCultureIgnoreCase) >= 0)
                        {
                            if (!string.IsNullOrEmpty(thing.WearLink))
                            {
                                if ((ud.Empty1 || ud.Empty2) || !ud.InRightSlot)
                                    return BuildRedirect($"{AppVars.Profile.FishHandTwo} одевается", thing.WearLink);

                                return BuildRedirect("Снимаем " + ud.Hand1, "main.php?get_id=57&uid=" + ud.Wid + "&s=0&vcode=" + ud.Vcod);
                            }
                        }
                    }
                }
            }

            AppVars.AutoFishWearUd = false;
            return null;

            /*
             *          hand1 = начинается с "Слот для оружия..."
             * 
                        0 - "male_15.gif"
             *          1 - "Умник"
             *          2 - "i_w23_206.gif:Шлем Орка:|0|0|30|0|70|0|40@i_w25_123.gif:Амулет Лезвий (ап):|0|0|1|0|0|0|55@i_w1_130.gif:Кристальный Меч:|31|36|0|59|70|0|100@i_w26_121.gif:Пояс Утраты (ап):|0|0|32|0|20|0|80@i_mag_003.gif:Восстановление 150 HP:|0|0|0|0|0|0|5@i_mag_003.gif:Восстановление 150 HP:|0|0|0|0|0|0|5@i_mag_006.gif:Восстановление 250 MP:|0|0|0|0|0|0|5@i_w21_132.gif:Сапоги Безумного Бога:|10|15|35|0|0|0|150@sl_r_0.gif:Слот для кармана@sl_r_1.gif:Слот для содержимого кармана@i_w80_126.gif:Наручи Смятения (ап):|6|9|34|5|40|0|80@i_w24_127.gif:Таинственные Перчатки (ап):|0|0|25|0|30|0|40@i_w4_113.gif:Нож Дворцового Стража (ап):|11|18|0|55|0|0|50@i_w22_135.gif:Кристальное Кольцо (ап):|0|0|8|0|30|0|70@i_w22_135.gif:Кристальное Кольцо (ап):|0|0|8|0|30|0|70@i_w19_174.gif:Доспех Преобладания:|0|0|65|0|90|0|120@"
             *          3 - "17303916@19267555@19689834@18454797@28166617@28014680@28352695@28122138@@@16887886@18909831@19976974@16964485@17500500@28122375@",
             *          4 - "0a05a7b2ceb5f3abc0690ed6220bdcb1@113d5dc28a1a24fccb3b501265ef48bb@5be474011f26ca6b8f99a5aa0090162e@bd914959504e204fd2033a4ee0f8f9ff@73e59e6d1c910e2f65c185ea40cab82f@e38da35cd7ba5d6b2dd4cb8671250fb7@48e77051a824827531ec358809e4286e@bbae6bf33183089e953640aa1fec4f1e@@@24f4013a00795d574d2ac54f8b27e0d6@b4acc0cb5bca29e0926a89b3be600e74@cb11eb56fa5cf7ba7eb6eefea749173f@a311ab066d920e30eb0e48885e92b664@b729770b2a863ffd946c74462cf395cd@bc073794482984f434ec27033abddf08@"
             *          5 - "40@55@100@80@4@5@5@150@@@80@40@50@70@70@120@"
             *          6 - 115

             * 
             * <input type=button class=invbut onclick="location='main.php?get_id=57&wid=27975541&vcode=787337e6dbe7e7c26bc662c2b8a7eaaa'" value="Надеть"> <input type=button class=invbut onclick="transferform('27975541','18','Телескопическая Облегченная Удочка','228480248dc96614aa4b22205cd3f966','600','8','271','600')" value="Передать"> <input type=button class=invbut onclick="presentform('27975541','Телескопическая Облегченная Удочка','00ca0b39ce2fd18562f6ac5dbf220c39','8','600','271','600')" value="Подарить"> <input type=button class=invbut onclick="sellingform('27975541','Телескопическая Облегченная Удочка','fe0471e803821822c2d426eaa7fc5db0','600','8','0')" value="Продать">
             * GET /main.php?get_id=58&wid=19689834&vcode=e948ed286ccc88e048fae70798d6c251 HTTP/1.1
            */

stopautofish:
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateFishOffDelegate(AppVars.MainForm.UpdateFishOff), new object[] { });
                }
            }
            catch (InvalidOperationException)
            {
            }

            return null;
        }

        private static IEnumerable<InvEntry> GetInvList(string html)
        {
            var invList = new List<InvEntry>();

            const string patternStartInv = "</b></font></td></tr>";
            var pos = html.IndexOf(patternStartInv, StringComparison.Ordinal);
            if (pos == -1)
                return invList;

            pos += patternStartInv.Length;
            while (true)
            {
                const string parrernStartTr = "<tr><td bgcolor=#F5F5F5>";
                if (!html.Substring(pos, parrernStartTr.Length).StartsWith(parrernStartTr, StringComparison.Ordinal))
                    break;

                const string parrernEndTr = "<td bgcolor=#FCFAF3><img src=http://image.neverlands.ru/1x1.gif width=5 height=1></td></tr></table></td></tr></table></td></tr>";
                var posEnd = html.IndexOf(parrernEndTr, pos, StringComparison.Ordinal);
                if (posEnd == -1)
                {
                    const string parrernEndTrShort = "<img src=http://image.neverlands.ru/1x1.gif width=1 height=5></td></tr></table></td></tr>";
                    posEnd = html.IndexOf(parrernEndTrShort, pos, StringComparison.Ordinal);
                    if (posEnd == -1)
                        return invList;

                    posEnd += parrernEndTrShort.Length;
                }
                else
                {
                    posEnd += parrernEndTr.Length;
                }

                var htmlEntry = html.Substring(pos, posEnd - pos);
                var invEntry = new InvEntry(htmlEntry);
                invList.Add(invEntry);
                pos = posEnd;
            }

            return invList;
        }

        private static bool MainPhpIsMustWearUd(string html)
        {
            var parsedDressed = new ParsedDressed(html);
            if (!parsedDressed.Valid)
            {
                return false;
            }

            // Проверяем, одета ли первая удочка
            var iswear1 = parsedDressed.IsWear1();

            // Если нет - должны одеть
            if (!iswear1 && AppVars.Profile.FishAutoWear)
            {
                return true;
            }

            // Проверяем, одета ли вторая удочка
            var iswear2 = parsedDressed.IsWear2();

            // Если нет - ищем и одеваем
            return !iswear2 && AppVars.Profile.FishAutoWear;
        }

        private static bool MainPhpArmedKinfe(string html)
        {
            var parsedDressed = new ParsedDressed(html);
            if (!parsedDressed.Valid)
            {
                return false;
            }

            var result = parsedDressed.IsWearKnife();
            return result;
        }

        private static string MainPhpWearKnife(string html)
        {
            var list = new[] { "Малый Разделочный Нож", "Охотничий Нож", "Вороненый Охотничий Нож", "Разделочный Топорик", "Арисайский Охотничий Нож" };

            var ud = new ParsedDressed(html);
            if (!ud.Valid)
                return null;

            var invList = GetInvList(html);
            var iswear = ud.IsWearKnife();
            if (!iswear)
            {
                foreach (var thing in invList)
                {
                    for (var j = 0; j < list.Length; j++)
                    {
                        if (thing.Name.IndexOf(list[j], StringComparison.CurrentCultureIgnoreCase) >= 0)
                        {
                            if (!string.IsNullOrEmpty(thing.WearLink))
                            {
                                return BuildRedirect($"Одеваем {thing.Name}", thing.WearLink);
                            }
                        }
                    }
                }
            }

            AppVars.AutoSkinArmedKnife = false;
            return null;
        }

        private static void MainPhpGetSkinRes(string html)
        {
            /*
                <B>Рост</B></td></tr>
                <tr><td bgcolor=#FFFFFF width=5% align=center><input type=checkbox name="res_213" value="213"></td>
                  <td bgcolor=#FFFFFF width=15% class=travma align=center>2.100</td>
                  <td bgcolor=#FFFFFF align=center><input type=text size=3 class=weaponch name="use_213" value="2"></td>
                  <td bgcolor=#FFFFFF align=center><img src=http://image.neverlands.ru/resources/213.gif width=60 height=60></td>
                  <td bgcolor=#FFFFFF width=25% class=travma align=center><B>Крысиный хвост</B><BR><font color=#008800><B>Срок годности:</B> 08.02.2016 17:04</font></td>
                  <td bgcolor=#FFFFFF width=10% class=travma align=center>15.00</td><td bgcolor=#FFFFFF width=25% class=travma align=center>0 / 0</td>
                  <td bgcolor=#FFFFFF width=20% class=travma align=center><B><img src=http://image.neverlands.ru/1x1.gif width=10 height=14>0.00%</B></td></tr>
                <tr>
                  <td bgcolor=#FCFAF3 width=5% align=center><input type=checkbox name="res_214" value="214"></td>
                  <td bgcolor=#FCFAF3 width=15% class=travma align=center>0.400</td>
                  <td bgcolor=#FCFAF3 align=center><input type=text size=3 class=weaponch name="use_214" value="0"></td>
                  <td bgcolor=#FCFAF3 align=center><img src=http://image.neverlands.ru/resources/214.gif width=60 height=60></td>
                  <td bgcolor=#FCFAF3 width=25% class=travma align=center><B>Крысиная лапа</B><BR><font color=#008800><B>Срок годности:</B> 08.02.2016 17:04</font></td>
                  <td bgcolor=#FCFAF3 width=10% class=travma align=center>15.00</td>
                  <td bgcolor=#FCFAF3 width=25% class=travma align=center>0 / 0</td>
                  <td bgcolor=#FCFAF3 width=20% class=travma align=center><B><img src=http://image.neverlands.ru/1x1.gif width=10 height=14>0.00%</B></td>
                </tr>
                </FORM></table></td></tr><tr><td><img src=http://image.neverlands.ru/1x1.gif width=1 height=3></td></tr><tr><td align=right>
            */

            const string patternStartRes = "<B>Рост</B></td></tr>";
            var pos = html.IndexOf(patternStartRes, StringComparison.Ordinal);
            if (pos == -1)
                return;

            var sb = new StringBuilder();

            pos += patternStartRes.Length;
            while (true)
            {
                const string parrernStartTr = "<input type=checkbox name=";
                pos = html.IndexOf(parrernStartTr, pos, StringComparison.Ordinal);
                if (pos == -1)
                    break;

                pos += parrernStartTr.Length;

                const string parrernEndTr = "</tr>";
                var posEnd = html.IndexOf(parrernEndTr, pos, StringComparison.Ordinal);
                if (posEnd != -1)
                {
                    posEnd += parrernEndTr.Length;
                }

                var htmlEntry = html.Substring(pos, posEnd - pos);

                var valString = HelperStrings.SubString(htmlEntry, " width=15% class=travma align=center>", "</td>");
                double val;
                if (double.TryParse(valString, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                {
                    var name = HelperStrings.SubString(htmlEntry, " width=25% class=travma align=center><B>", "</B><BR>");
                    if (!string.IsNullOrEmpty(name))
                    {
                        if (sb.Length > 0)
                            sb.Append(", ");

                        if (AppVars.SkinRes.ContainsKey(name))
                        {
                            if (Math.Abs(AppVars.SkinRes[name] - val) > 0.009)
                            {
                                var diff = val - AppVars.SkinRes[name];
                                sb.Append($"<span style=\"color:#009933;font-weight:bold;\">«{name} {val:F2}»</span> (+{diff:F2})");
                                AppVars.SkinRes[name] = val;
                            }
                            else
                            {
                                sb.Append($"<span style=\"color:#009933;font-weight:bold;\">«{name} {val:F2}»</span>");
                            }
                        }
                        else
                        {
                            sb.Append($"<span style=\"color:#009933;font-weight:bold;\">«{name} {val:F2}»</span>");
                            AppVars.SkinRes.Add(name, val);
                        }
                    }
                }

                pos = posEnd;
            }

            if (sb.Length > 0)
            {
                sb.Insert(0, "Охотничьи ресурсы: ");
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateWriteChatMsgDelegate(AppVars.MainForm.WriteChatMsg), sb.ToString());
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
        }
    }
}