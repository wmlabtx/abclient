namespace ABClient.PostFilter
{
    using System;
    using ABForms;
    using Helpers;
    using MyHelpers;
    using System.Text;
    using MyChat;
    using System.Text.RegularExpressions;

    internal static partial class Filter
    {
        private static byte[] TradePhp(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);

            if (html.IndexOf(@"onclick=""location='../main.php'"" value=""Отказаться от покупки"">", StringComparison.OrdinalIgnoreCase) == -1)
            {
                return array;
            }

            var salesNick = HelperStrings.SubString(html, @"<font color=#cc0000>Купить вещь у ", " за ");
            var strSalesPrice = HelperStrings.SubString(html, @" за ", "NV?</font>");
            if (strSalesPrice == null)
            {
                return array;
            }

            int salesPrice;
            if (!int.TryParse(strSalesPrice, out salesPrice))
            {
                return array;
            }

            var nameThing = HelperStrings.SubString(html, @"NV?</font><br><br> ", "</b>");
            var levelThing = HelperStrings.SubString(html, @"&nbsp;Уровень: <b>", "</b>");
            var intLevelThing = -1;
            if (levelThing != null)
            {
                if (!int.TryParse(levelThing , out intLevelThing))
                {
                    intLevelThing = -1;
                }

                levelThing = '[' + levelThing + ']';
            }

            var uidThing = HelperStrings.SubString(html, @"&tradeu=", "&");
            if (uidThing == null)
            {
                return array;
            }

            var regS = new Regex(@"Долговечность: <b>([\d]+)/([\d]+)</b>");
            var matchS = regS.Match(html);
            if (matchS.Groups.Count != 3)
            {
                return array;
            }

            int realDolg;
            if (!int.TryParse(matchS.Groups[1].Value, out realDolg))
            {
                return array;
            }

            int fullDolg;
            if (!int.TryParse(matchS.Groups[2].Value, out fullDolg))
            {
                return array;
            }

            if (fullDolg == 0)
            {
                return array;
            }

            var strRealPrice = HelperStrings.SubString(html, @"&nbsp;Цена: <b>", " NV</b>");
            if (strRealPrice == null)
            {
                return array;
            }

            int realPrice;
            if (!int.TryParse(strRealPrice, out realPrice))
            {
                return array;
            }

            // Теперь считаем цену продажи            

            if (realDolg < fullDolg)
            {
                realPrice = (realPrice*realDolg)/fullDolg;
            }

            var sb = new StringBuilder();
            sb.Append("%<");
            sb.Append(salesNick);
            sb.Append("> ");

            var sbmsg = new StringBuilder();
            sbmsg.Append(" <b>");
            sbmsg.Append(nameThing);
            sbmsg.Append(levelThing);
            sbmsg.Append("</b>, долговечность <b>");
            sbmsg.Append(realDolg);
            sbmsg.Append('/');
            sbmsg.Append(fullDolg);
            sbmsg.Append("</b>, (госцена <font color=#00cc00><b>");
            sbmsg.Append(realPrice);
            sbmsg.Append("NV</b></font>) за <font color=#0000cc><b>");
            sbmsg.Append(salesPrice);
            sbmsg.Append("NV</b></font> у <b>");
            sbmsg.Append(salesNick);
            sbmsg.Append("</b>");

            var price90 = (int)Math.Round(realPrice * 0.9);
            var calcPrice = TorgList.Calculate(realPrice);
            if (salesPrice > calcPrice)
            {
                // Слишком высокая цена
                
                try
                {
                    if (AppVars.MainForm != null)
                        AppVars.MainForm.BeginInvoke(
                            new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                            new object[] { "<font color=#cc0000><b>Отказываемся</b></font> от покупки вещи" + sbmsg });
                }
                catch (InvalidOperationException)
                {
                }

                sb.Append(TorgList.DoFilter(AppVars.Profile.TorgMessageTooExp, nameThing, levelThing, salesPrice, calcPrice, realDolg, fullDolg, price90));
                Chat.AddAnswer(sb.ToString());
                AppVars.ContentMainPhp = BuildRedirect("Отказ от покупки", "../main.php");
                return Russian.Codepage.GetBytes(AppVars.ContentMainPhp);
            }
            
            if (salesPrice < price90)
            {
                // Слишком низкая цена
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                            new object[] { "<font color=#cc0000><b>Отказываемся</b></font> от покупки вещи" + sbmsg });
                    }
                }
                catch (InvalidOperationException)
                {
                }

                sb.Append(TorgList.DoFilter(AppVars.Profile.TorgMessageLess90, nameThing, levelThing, salesPrice, calcPrice, realDolg, fullDolg, price90));
                Chat.AddAnswer(sb.ToString());
                AppVars.ContentMainPhp = BuildRedirect("Отказ от покупки", "../main.php");
                return Russian.Codepage.GetBytes(AppVars.ContentMainPhp);
            }

            var spdeny = AppVars.Profile.TorgDeny.Trim().Split(';');
            for (var i = 0; i < spdeny.Length; i++)
            {
                if (string.IsNullOrEmpty(spdeny[i]))
                {
                    continue;
                }

                var keydeny = spdeny[i].Trim();
                if ((keydeny.IndexOf(' ') == -1 && nameThing.IndexOf(keydeny, StringComparison.OrdinalIgnoreCase) != -1) ||
                    (keydeny.IndexOf(' ') != -1 && nameThing.Equals(keydeny, StringComparison.OrdinalIgnoreCase)))
                {
                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.BeginInvoke(
                                new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                                new object[] { "В имени вещи содержится ключевое слово <b>" + keydeny + "</b>, указанное в настройках. Отказываемся от покупки." });
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }

                    AppVars.ContentMainPhp = BuildRedirect("Отказ от покупки", "../main.php");
                    return Russian.Codepage.GetBytes(AppVars.ContentMainPhp);
                }
            }

            // Делаем попытку купить

            var linkPrefix = "../main.php?get_id=0";
            var link = HelperStrings.SubString(html, linkPrefix, "'");
            if (link == null)
            {
                return array;
            }

            // Готовимся
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                        new object[] { "<font color=#00cc00><b>Покупаем</b></font> вещь" + sbmsg });
                }
            }
            catch (InvalidOperationException)
            {
            }

            TorgList.TriggerBuy = false;
            if (AppVars.Profile.TorgSliv)
            {
                if (intLevelThing < AppVars.Profile.TorgMinLevel)
                {
                    if (nameThing.IndexOf("(ап)", StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        var isDisabled = false;
                        var keyword = string.Empty;
                        if (!string.IsNullOrEmpty(AppVars.Profile.TorgEx))
                        {
                            var sp = AppVars.Profile.TorgEx.Trim().Split(';');
                            var i = 0;
                            for (; i < sp.Length; i++)
                            {
                                keyword = sp[i].Trim();
                                if ((keyword.IndexOf(' ') == -1 && nameThing.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) == -1) ||
                                    (keyword.IndexOf(' ') != -1 && !nameThing.Equals(keyword, StringComparison.OrdinalIgnoreCase)))
                                {
                                    continue;
                                }
                                
                                isDisabled = true;
                                break;
                            }
                        }

                        if (!isDisabled)
                        {
                            try
                            {
                                if (AppVars.MainForm != null)
                                {
                                    AppVars.MainForm.BeginInvoke(
                                        new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                                        new object[] { "Делаем попытку сдать вещь в лавку..." });
                                }
                            }
                            catch (InvalidOperationException)
                            {
                            }

                            TorgList.UidThing = uidThing;
                            TorgList.TriggerBuy = true;
                        }
                        else
                        {
                            try
                            {
                                if (AppVars.MainForm != null)
                                {
                                    AppVars.MainForm.BeginInvoke(
                                        new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                                        new object[] { "В имени вещи содержится ключевое слово <b>" + keyword + "</b>, указанное в настройках. Оставляем вещь себе." });
                                }
                            }
                            catch (InvalidOperationException)
                            {
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                                    new object[] { "Вещь апнута, оставляем ее себе" });
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }
                }
                else
                {
                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.BeginInvoke(
                                new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                                new object[] { "Уровень вещи <b>[" + intLevelThing + "]</b> равен или превышает уровень <b>[" + AppVars.Profile.TorgMinLevel + "]</b>, указанный в настройках. Оставляем вещь себе." });
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }
            }
            else
            {
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                            new object[] {"Перепродажа вещей в лавку отключена в настройках"});
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }

            var sbyes = new StringBuilder(sb.ToString());
            sbyes.Append(TorgList.DoFilter(AppVars.Profile.TorgMessageThanks, nameThing, levelThing, salesPrice, calcPrice, realDolg, fullDolg, price90));
            TorgList.MessageThanks = sbyes.ToString();

            var sbno = new StringBuilder(sb.ToString());
            sbno.Append(TorgList.DoFilter(AppVars.Profile.TorgMessageNoMoney, nameThing, levelThing, salesPrice, calcPrice, realDolg, fullDolg, price90));
            TorgList.MessageNoMoney = sbno.ToString();

            TorgList.Trigger = true;

            AppVars.ContentMainPhp = BuildRedirect("Покупка", linkPrefix + link);
            return Russian.Codepage.GetBytes(AppVars.ContentMainPhp);
        }
    }
}
