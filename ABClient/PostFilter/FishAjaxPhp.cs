namespace ABClient.PostFilter
{
    using System;
    using System.Globalization;
    using System.Text;
    using ABForms;
    using MyHelpers;

    internal static partial class Filter
    {
        internal static byte[] FishAjaxPhp(byte[] array)
        {
            AppVars.PriSelected = false;
            var html = AppVars.Codepage.GetString(array);
            /* AL@["Вид ресурса: рыба «Карп».<br>Клёв: 4 шт.<br>Улов: 3 шт."]@ */
            /* RESO@[""]@[]@[["ogl","Оглядеться","389337399b970e3ca06ff8f49d125bcb",[]],["fis","Рыбалка","0c4a73c4b140c0287a6bc2fffa2537d4",[]],["dri","Пить","1b9918ef796382fcde609cab672c5fcf",[]]]@[0,[2,30]]@[1,"136638010150555cdfe33a9","16288f747b7eaeccaa7cdd7fdd9885ec",395.00,437,[38,"Хлеб",25]] */

            /* RESO@["Вид ресурса: «Пескарь».<br>Клёв: 3 шт.<br>Улов: 3 шт.<BR>Умение «Рыбалка» повысилось на 1!"]@[]@[["ogl","Оглядеться","b8d22a61080c7725e83bb78f45c1b4d2",[]],["fis","Рыбалка","2bfac552f4cc6b32898aa43de3c25502",[]],["dri","Пить","c5629ecc126e5c005f779f31eadf90e3",[]]]@[0,[2,294]]@[] */

            //html = "RESO@[\"Вид ресурса: «Пескарь».<br>Клёв: 3 шт.<br>Улов: 3 шт.<BR>Умение «Рыбалка» повысилось на 1!\"]@[]@[[\"ogl\",\"Оглядеться\",\"b8d22a61080c7725e83bb78f45c1b4d2\",[]],[\"fis\",\"Рыбалка\",\"2bfac552f4cc6b32898aa43de3c25502\",[]],[\"dri\",\"Пить\",\"c5629ecc126e5c005f779f31eadf90e3\",[]]]@[0,[2,294]]@[]";

            if (
                html.IndexOf("У Вас нет рыболовных снастей.", StringComparison.OrdinalIgnoreCase) != -1 ||
                html.IndexOf("У Вас нет приманки, чтобы ловить рыбу.", StringComparison.OrdinalIgnoreCase) != -1 ||
                html.IndexOf("Приманок нет в наличии.", StringComparison.OrdinalIgnoreCase) != -1 ||
                html.IndexOf("У Вас не хватает умения, чтобы ловить тут рыбу.", StringComparison.OrdinalIgnoreCase) != -1)
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

                return array;
            }

            var posOpenBracket = html.IndexOf('"');
            if (posOpenBracket == -1)
            {
                return array;
            }

            posOpenBracket++;
            var posCloseBracket = html.IndexOf('"', posOpenBracket);
            if (posOpenBracket == -1)
            {
                return array;
            }

            if (html.IndexOf("лёв:", StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                var newString = FishReport(html);
                if (!string.IsNullOrEmpty(newString))
                {
                    html = html.Substring(0, posOpenBracket) + newString + html.Substring(posCloseBracket);
                }

                array = AppVars.Codepage.GetBytes(html);
            }

            return array;
        }

        private static string FishReport(string html)
        {
            var numFish = 0;
            var catchFish = 0;
            var fishUmUp = html.IndexOf("повысилось на 1!", StringComparison.OrdinalIgnoreCase) != -1;

            AppVars.AutoFishCheckUm = html.IndexOf("повысилось на 1!", StringComparison.OrdinalIgnoreCase) != -1 || AppVars.Profile.FishUm == 0;
            int p1 = html.IndexOf('«');
            if (p1 == -1)
            {
                return string.Empty;
            }

            var p2 = html.IndexOf('»', p1);
            if (p2 == -1)
            {
                return string.Empty;
            }

            string nameFish = html.Substring(p1 + 1, p2 - p1 - 1);
            var strNumFish = HelperStrings.SubString(html, "Улов: ", " шт.");
            if (!string.IsNullOrEmpty(strNumFish))
            {
                if (!int.TryParse(strNumFish, out numFish))
                {
                    numFish = 0;
                }
            }

            var strCatchFish = HelperStrings.SubString(html, "Клёв: ", " шт.");
            if (!string.IsNullOrEmpty(strCatchFish))
            {
                if (!int.TryParse(strCatchFish, out catchFish))
                {
                    catchFish = 0;
                }
            }

            var staticFishReport = "Нет клёва.";
            var posf = html.IndexOf(staticFishReport, StringComparison.OrdinalIgnoreCase);
            if (posf != -1)
            {
                catchFish = 0;
            }
            else
            {
                staticFishReport = "Не удалось вытащить рыбу.";
                posf = html.IndexOf(staticFishReport, StringComparison.OrdinalIgnoreCase);
                if (posf != -1)
                {
                    return string.Empty;
                }
            }

            var sbr = new StringBuilder();
            var s2 = new StringBuilder();

            sbr.Append("<b>");
            sbr.Append(nameFish);
            sbr.Append("</b> [<b>");
            sbr.Append(numFish);
            sbr.Append('/');
            sbr.Append(catchFish);
            sbr.Append("</b>]. ");

            if (fishUmUp)
            {
                AppVars.Profile.FishUm++;
            }

            if (AppVars.Profile.FishUm > 0)
            {
                sbr.Append("Умелка");
                s2.Append("Умелка");
                sbr.Append(":&nbsp;<b>");
                s2.Append(": ");
                sbr.Append(AppVars.Profile.FishUm);
                sbr.Append("</b>");
                s2.Append(AppVars.Profile.FishUm);
                if (fishUmUp)
                {
                    sbr.Append("&nbsp;(<font color=#008800><b>+");
                    s2.Append(" (+");
                    sbr.Append(1);
                    s2.Append(1);
                    sbr.Append("<img src=http://image.neverlands.ru/gameplay/up.gif width=10 height=14></b></font>)");
                    s2.Append(")");
                }

                s2.AppendLine();
            }

            double fishp = 0;
            double fishmassa = 0;
            switch (nameFish)
            {
                case "Карась":
                    fishp = 4.32;
                    fishmassa = 2;
                    break;
                case "Плотва":
                    fishp = 3.62;
                    fishmassa = 2;
                    break;
                case "Пескарь":
                    fishp = 3.94;
                    fishmassa = 2;
                    break;
                case "Щука":
                    fishp = 23.15;
                    fishmassa = 5;
                    break;
                case "Ёрш":
                    fishp = 3.34;
                    fishmassa = 2;
                    break;
                case "Окунь":
                    fishp = 11.54;
                    fishmassa = 2;
                    break;
                case "Краснопёрка":
                    fishp = 8.58;
                    fishmassa = 2;
                    break;
                case "Налим":
                    fishp = 23.85;
                    fishmassa = 3;
                    break;
                case "Судак":
                    fishp = 13.14;
                    fishmassa = 2;
                    break;
                case "Верхоплавка":
                    fishp = 2.68;
                    fishmassa = 2;
                    break;
                case "Лещ":
                    fishp = 22.20;
                    fishmassa = 2;
                    break;
                case "Подлещик":
                    fishp = 4.76;
                    fishmassa = 2;
                    break;
                case "Карп":
                    fishp = 5.26;
                    fishmassa = 2;
                    break;
                case "Форель":
                    fishp = 29.75;
                    fishmassa = 5;
                    break;
                case "Бычок":
                    fishp = 8.80;
                    fishmassa = 2;
                    break;
                case "Голавль":
                    fishp = 7.26;
                    fishmassa = 2;
                    break;
                case "Линь":
                    fishp = 31.62;
                    fishmassa = 2;
                    break;
                case "Сом":
                    fishp = 42.04;
                    fishmassa = 4;
                    break;
                case "Язь":
                    fishp = 29.12;
                    fishmassa = 2;
                    break;
            }

            double prim = 0;
            double primassa = 0;
            var namepri = AppVars.NamePri;
            switch (namepri)
            {
                case "Хлеб":
                    prim = 1;
                    primassa = 0.2;
                    break;
                case "Червяк":
                    prim = 1;
                    primassa = 0.1;
                    break;
                case "Крупный червяк":
                    prim = 1;
                    primassa = 0.2;
                    break;
                case "Опарыш":
                    prim = 5;
                    primassa = 0.1;
                    break;
                case "Мотыль":
                    prim = 5;
                    primassa = 0.1;
                    break;
                case "Блесна":
                    prim = 10;
                    primassa = 0.3;
                    break;
                case "Донка":
                    prim = 12;
                    primassa = 0.3;
                    break;
                case "Мормышка":
                    prim = 15;
                    primassa = 0.3;
                    break;
                case "Заговоренная блесна":
                    prim = 20;
                    primassa = 0.4;
                    break;
            }

            double bal = 0;
            if (!string.IsNullOrEmpty(AppVars.AutoFishHand1))
            {
                sbr.Append("<br>");
                sbr.Append("Долговечность");
                s2.Append(AppVars.AutoFishHand1);
                sbr.Append(":&nbsp;<b>");
                s2.Append(" (до заброса): ");
                sbr.Append(AppVars.AutoFishHand1D);
                s2.Append(AppVars.AutoFishHand1D);
                sbr.Append("</b>");
                bal -= 2.5;
                s2.AppendLine();
            }

            /*
            if (!string.IsNullOrEmpty(AppVars.AutoFishHand2))
            {
                sbr.Append("<br>");
                sbr.Append(AppVars.AutoFishHand2);
                s2.Append(AppVars.AutoFishHand2);
                sbr.Append("&nbsp;(до заброса):</td><td bgcolor=#FAFAFA nowrap class=nickname><b>&nbsp;");
                s2.Append(" (до заброса): ");
                sbr.Append(AppVars.AutoFishHand2D);
                s2.Append(AppVars.AutoFishHand2D);
                bal -= 2.5;
                sbr.Append("&nbsp;</b></td></tr>");
                s2.AppendLine();
            }
             */

            if (!string.IsNullOrEmpty(AppVars.AutoFishMassa) && ((numFish > 0) || (catchFish > 0)))
            {
                sbr.Append("<br>");
                sbr.Append("Масса");
                s2.Append("Масса");
                sbr.Append(":&nbsp;<b>");
                s2.Append(": ");

                var d2 = (fishmassa * numFish) - (primassa * catchFish);
                var sf = AppVars.AutoFishMassa.Split('/');
                var cur = Convert.ToDouble(sf[0], CultureInfo.InvariantCulture) + d2;
                AppVars.AutoFishMassa = cur.ToString("###0.##", CultureInfo.InvariantCulture) + "/" + sf[1];
                sbr.Append(AppVars.AutoFishMassa);
                if (d2 != 0)
                {
                    var sd2 = d2.ToString("###0.##", CultureInfo.InvariantCulture);
                    if (d2 < 0)
                    {
                        sbr.Append("</b>&nbsp;(<font color=#CC0000><b>");
                        s2.Append(" (");
                        sbr.Append(sd2);
                        s2.Append(sd2);
                        sbr.Append("<img src=http://image.neverlands.ru/gameplay/down.gif width=10 height=14></b></font>)");
                        s2.Append(")");
                    }
                    else
                    {
                        sbr.Append("</b>&nbsp;(<font color=#008800><b>+");
                        s2.Append(" (+");
                        sbr.Append(sd2);
                        s2.Append(sd2);
                        sbr.Append("<img src=http://image.neverlands.ru/gameplay/up.gif width=10 height=14></b></font>)");
                        s2.Append(")");
                    }
                }

                s2.AppendLine();
            }

            if (!string.IsNullOrEmpty(namepri))
            {
                sbr.Append("<br><b>");
                sbr.Append(namepri);
                s2.Append(namepri);
                sbr.Append("</b>&nbsp;(остаток):&nbsp;<b>");
                s2.Append(" (остаток): ");
                var cur = AppVars.ValPri - catchFish;
                sbr.Append(cur);
                s2.Append(cur);
                if (catchFish > 0)
                {
                    sbr.Append("</b>&nbsp;(<font color=#CC0000><b>-");
                    s2.Append(" (-");
                    sbr.Append(catchFish);
                    s2.Append(catchFish);
                    sbr.Append("<img src=http://image.neverlands.ru/gameplay/down.gif width=10 height=14></b></font>)");
                    s2.Append(")");
                }

                bal -= prim * catchFish;
                s2.AppendLine();
            }

            if (!string.IsNullOrEmpty(nameFish))
            {
                s2.Append(nameFish);
                s2.Append(" (улов): ");
                s2.Append(numFish);
                bal += fishp * numFish;
                s2.AppendLine();
            }

            s2.Append(bal < 0 ? "Потери" : "Доход");
            s2.Append(": ");
            var sbal = bal.ToString("###0.##", CultureInfo.InvariantCulture);
            if (bal < 0)
            {
                // sbr.Append("<font color=#CC0000><b>");
                // sbr.Append(sbal);
                s2.Append(sbal);
                // sbr.Append("<img src=http://image.neverlands.ru/gameplay/down.gif width=10 height=14></b></font>&nbsp;NV");
                s2.Append(" NV");
            }
            else
            {
                // sbr.Append("<font color=#008800><b>+");
                s2.Append("+");
                // sbr.Append(sbal);
                s2.Append(sbal);
                // sbr.Append("<img src=http://image.neverlands.ru/gameplay/up.gif width=10 height=14></b></font>&nbsp;NV");
                s2.Append(" NV");
            }

            s2.AppendLine();

            AppVars.AutoFishNV += bal;

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateFishNVIncDelegate(AppVars.MainForm.UpdateFishNV),
                        new object[] { (int)bal });
                }
            }
            catch (InvalidOperationException)
            {
            }

            sbr.Append("<br>");
            if (AppVars.AutoFishNV < 0)
            {
                sbr.Append("Потери&nbsp;за&nbsp;рыбалку");
                s2.Append("Потери за рыбалку");
            }
            else
            {
                sbr.Append("Доход&nbsp;за&nbsp;рыбалку");
                s2.Append("Доход за рыбалку");
            }

            sbr.Append(":&nbsp;");
            s2.Append(": ");
            sbal = AppVars.AutoFishNV.ToString("###0.##", CultureInfo.InvariantCulture);
            if (AppVars.AutoFishNV < 0)
            {
                sbr.Append("<font color=#CC0000><b>");
                sbr.Append(sbal);
                s2.Append(sbal);
                sbr.Append("<img src=http://image.neverlands.ru/gameplay/down.gif width=10 height=14></b></font>&nbsp;NV");
                s2.Append(" NV");
            }
            else
            {
                sbr.Append("<font color=#008800><b>+");
                s2.Append("+");
                sbr.Append(sbal);
                s2.Append(sbal);
                sbr.Append("<img src=http://image.neverlands.ru/gameplay/up.gif width=10 height=14></b></font>&nbsp;NV");
                s2.Append(" NV");
            }

            if (AppVars.Profile.ShowTrayBaloons)
            {
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateTrayBaloonDelegate(AppVars.MainForm.UpdateTrayBaloon),
                            new object[] { s2.ToString() });
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }

            if (!string.IsNullOrEmpty(nameFish) && AppVars.Profile.FishChatReport)
            {
                var sbchat = new StringBuilder();
                //sbchat.Append(AppVars.AppVersion.ProductShortVersion);
                //sbchat.Append(": ");
                sbchat.Append("Умелка ");
                sbchat.Append(AppVars.Profile.FishUm);
                sbchat.Append(". ");
                sbchat.Append(namepri);
                if (!string.IsNullOrEmpty(AppVars.AutoFishHand1) && !string.IsNullOrEmpty(AppVars.AutoFishHand2))
                {
                    if (AppVars.AutoFishHand1.Equals("Сачок", StringComparison.OrdinalIgnoreCase) ||
                        AppVars.AutoFishHand2.Equals("Сачок", StringComparison.OrdinalIgnoreCase))
                    {
                        sbchat.Append("+Сачок");
                    }
                }

                sbchat.Append(" » ");
                sbchat.Append(nameFish);
                sbchat.Append(" [");
                sbchat.Append(numFish);
                sbchat.Append('/');
                sbchat.Append(catchFish);
                sbchat.Append("]. ");
                if (AppVars.AutoFishNV < 0)
                {
                    sbchat.Append("Потери");
                }
                else
                {
                    sbchat.Append("Доход");
                }

                sbchat.Append(" за сеанс: ");
                sbchat.Append(AppVars.AutoFishNV.ToString("###0.##", CultureInfo.InvariantCulture));
                sbchat.Append(" NV.");
                if (fishUmUp)
                {
                    sbchat.Append(@" Умение ""Рыбалка"" повысилось на 1!");
                }

                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateWriteRealChatMsgDelegate(AppVars.MainForm.WriteMessageToChat),
                            new object[] { sbchat.ToString() });
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }

            if (!string.IsNullOrEmpty(nameFish) && AppVars.Profile.FishChatReportColor)
            {
                var sbchat = new StringBuilder();
                sbchat.Append("Умелка <b>");
                sbchat.Append(AppVars.Profile.FishUm);
                sbchat.Append("</b>. ");
                sbchat.Append(namepri);
                if (!string.IsNullOrEmpty(AppVars.AutoFishHand1) && !string.IsNullOrEmpty(AppVars.AutoFishHand2))
                {
                    if (AppVars.AutoFishHand1.Equals("Сачок", StringComparison.OrdinalIgnoreCase) ||
                        AppVars.AutoFishHand2.Equals("Сачок", StringComparison.OrdinalIgnoreCase))
                    {
                        sbchat.Append("+Сачок");
                    }
                }

                sbchat.Append(" » ");
                sbchat.Append(nameFish);
                sbchat.Append(" [");
                sbchat.Append(numFish);
                sbchat.Append('/');
                sbchat.Append(catchFish);
                sbchat.Append("]. ");
                if (AppVars.AutoFishNV < 0)
                {
                    sbchat.Append("Потери");
                }
                else
                {
                    sbchat.Append("Доход");
                }

                sbchat.Append(": <b>");
                sbchat.Append(AppVars.AutoFishNV.ToString("###0.##", CultureInfo.InvariantCulture));
                sbchat.Append(" NV</b>.");
                if (fishUmUp)
                {
                    sbchat.Append(@" Умение ""Рыбалка"" <b>повысилось на 1</b>!");
                }

                if (AppVars.Profile.FishChatReportColor)
                {
                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.BeginInvoke(
                                new UpdateWriteChatMsgDelegate(AppVars.MainForm.WriteChatMsg),
                                new object[] { sbchat.ToString() });
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }
            }

            AppVars.AutoFishCheckUd = true;
            AppVars.AutoFishWearUd = false;
            return sbr.ToString();
        }
    }
}