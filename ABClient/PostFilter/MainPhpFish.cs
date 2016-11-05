namespace ABClient.PostFilter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using ABForms;
    using Helpers;
    using MyHelpers;
    using MySounds;
   
    // AL@["Вид ресурса: рыба «Карп».<br>Клёв: 1 шт.<br>Улов: 0 шт."]@[]@[["ogl","Оглядеться","f8dbb6f451bce8b76594aeef4d8122fa",[]],["fis","Рыбалка","602f954468e63529e8b3834980ec2734",[]],["dri","Пить","34d01f35cba8a0e7e2ab5117522251da",[]]]@[0,[2,294]]@[]
    
    internal static partial class Filter
    {
        private static string MainPhpAutoFishPrepare(string html)
        {
            var p1 = html.IndexOf(AppConsts.HtmlValueRiba, StringComparison.OrdinalIgnoreCase);
            if (p1 == -1)
            {
                return string.Empty;
            }

            AppVars.CodeAddress = string.Empty;
            var pcode = html.IndexOf(AppConsts.HtmlCodePhp, StringComparison.OrdinalIgnoreCase);
            if (pcode != -1)
            {
                pcode += AppConsts.HtmlCodePhp.Length;
                var pe = html.IndexOf('"', pcode);
                if (pe == -1)
                {
                    return string.Empty;
                }

                AppVars.CodeAddress = AppConsts.HtmlCodePhpFull + html.Substring(pcode, pe - pcode);
            }

            var getid = HelperStrings.SubString(html, "=get_id value=", ">");
            if (string.IsNullOrEmpty(getid))
            {
                return string.Empty;
            }

            var act = HelperStrings.SubString(html, "=act value=", ">");
            if (string.IsNullOrEmpty(act))
            {
                return string.Empty;
            }

            var vcode = HelperStrings.SubString(html, "=vcode value=", ">");
            if (string.IsNullOrEmpty(vcode))
            {
                return string.Empty;
            }

            var lakeid = HelperStrings.SubString(html, "=lakeid value=", ">");
            if (string.IsNullOrEmpty(lakeid))
            {
                return string.Empty;
            }

            AppVars.AutoFishMassa = HelperStrings.SubString(html, "<b>Масса Вашего инвентаря: ", "</b>");
            if (string.IsNullOrEmpty(AppVars.AutoFishMassa))
            {
                return string.Empty;
            }

            var primid = string.Empty;
            var l1 = new List<string>();
            if ((AppVars.Profile.FishEnabledPrims & Prims.Bread) != 0)
            {
                l1.Add("Хлеб");
            }

            if ((AppVars.Profile.FishEnabledPrims & Prims.Worm) != 0)
            {
                l1.Add("Червяк");
            }

            if ((AppVars.Profile.FishEnabledPrims & Prims.BigWorm) != 0)
            {
                l1.Add("Крупный червяк");
            }

            if ((AppVars.Profile.FishEnabledPrims & Prims.Stink) != 0)
            {
                l1.Add("Опарыш");
            }

            if ((AppVars.Profile.FishEnabledPrims & Prims.Fly) != 0)
            {
                l1.Add("Мотыль");
            }

            if ((AppVars.Profile.FishEnabledPrims & Prims.Light) != 0)
            {
                l1.Add("Блесна");
            }

            if ((AppVars.Profile.FishEnabledPrims & Prims.Donka) != 0)
            {
                l1.Add("Донка");
            }

            if ((AppVars.Profile.FishEnabledPrims & Prims.Morm) != 0)
            {
                l1.Add("Мормышка");
            }

            if ((AppVars.Profile.FishEnabledPrims & Prims.HiFlight) != 0)
            {
                l1.Add("Заговоренная блесна");
            }

            var l2 = new List<string>();
            while (l1.Count > 0)
            {
                var n = Dice.Make(l1.Count);
                l2.Add(l1[n]);
                l1.RemoveAt(n);
            }

            while (l2.Count > 0)
            {
                var pr = string.Empty;
                switch (l2[0])
                {
                    case "Хлеб":
                        pr = "38";
                        primid = "38";
                        break;
                    case "Червяк":
                        pr = "39";
                        primid = "39";
                        break;
                    case "Крупный червяк":
                        pr = "40";
                        primid = "40";
                        break;
                    case "Опарыш":
                        pr = "41";
                        primid = "41";
                        break;
                    case "Мотыль":
                        pr = "42";
                        primid = "42";
                        break;
                    case "Блесна":
                        pr = "43";
                        primid = "43";
                        break;
                    case "Донка":
                        pr = "44";
                        primid = "44";
                        break;
                    case "Мормышка":
                        pr = "45";
                        primid = "45";
                        break;
                    case "Заговоренная блесна":
                        pr = "46";
                        primid = "46";
                        break;
                }

                var temp =
                    "<input type=radio name=primid value=" +
                    primid +
                    "></td><td bgcolor=#FFFFFF><img src=http://image.neverlands.ru/tools/" +
                    pr +
                    ".gif width=60 height=60></td><td bgcolor=#FFFFFF align=center class=nickname><b>" +
                    l2[0] +
                    "</b></td><td bgcolor=#FFFFFF align=center class=nickname><b>";
                var pos = html.IndexOf(temp, StringComparison.OrdinalIgnoreCase);
                if (pos != -1)
                {
                    AppVars.AutoFishLikeId = primid;
                    AppVars.AutoFishLikeVal = HelperStrings.SubString(html, temp, "</b>");
                    if (pos != -1)
                    {
                        html = html.Insert(pos + 18, "checked ");
                    }

                    break;
                }

                primid = string.Empty;
                l2.RemoveAt(0);
            }

            if (string.IsNullOrEmpty(primid))
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

                return string.Empty;
            }

            /*
             * Без капчи
             *  /main.php?get_id=55&lakeid=2&act=4&primid=40&vcode=26ed9c9afae6128894a60e1b8275ebf4
             *  
             *  /main.php?get_id=55&lakeid=1&act=4&primid=39&code=57397&vcode=e026c7b0ccf87e33cff2b6907f22679b
             */
            AppVars.FightLink =
                "http://www.neverlands.ru/main.php?get_id=" +
                getid +
                "&lakeid=" +
                lakeid +
                "&act=" +
                act +
                "&primid=" +
                primid +
                (string.IsNullOrEmpty(AppVars.CodeAddress) ? string.Empty : "&code=????") +
                "&vcode=" +
                vcode;

            if (!string.IsNullOrEmpty(AppVars.CodeAddress))
            {
                if (!AppVars.Profile.DoGuamod)
                {
                    if (AppVars.MainForm != null && AppVars.MainForm.TrayIsDigitsWaitTooLong())
                    {
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateGuamodTurnOnDelegate(AppVars.MainForm.UpdateGuamodTurnOn),
                                    new object[] { });
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }
                    else
                    {
                        EventSounds.PlayDigits();
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateTrayFlashDelegate(AppVars.MainForm.UpdateTrayFlash),
                                    new object[] { "Ввод цифр" });
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }

                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.BeginInvoke(
                                new UpdateTrayFlashDelegate(AppVars.MainForm.UpdateTrayFlash),
                                new object[] { "Ввод цифр" });
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }
            }

            return html;
        }

        private static string MainPhpFishReport(string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException("html");
            }

            var nameFish = string.Empty;
            var numFish = 0;
            var catchFish = 0;
            var fishUmUp = false;
            int p1;
            var staticFishReport = @"<br><font color=#CC0000><b>Вид ресурса: рыба ";
            var strFishReportEnd = "</b></font><br></font><br>";
            var posf = html.IndexOf(staticFishReport, StringComparison.OrdinalIgnoreCase);
            if (posf != -1)
            {
                if (html.IndexOf("повысилось на 1!", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    fishUmUp = true;
                }

                AppVars.AutoFishCheckUm = html.IndexOf("повысилось на 1!", StringComparison.OrdinalIgnoreCase) != -1 || AppVars.Profile.FishUm == 0;
                p1 = html.IndexOf('«', posf);
                if (p1 == -1)
                {
                    return string.Empty;
                }

                var p2 = html.IndexOf('»', p1);
                if (p2 == -1)
                {
                    return string.Empty;
                }
                
                nameFish = html.Substring(p1 + 1, p2 - p1 - 1);
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

                var sb = new StringBuilder();
                if (catchFish == numFish)
                {
                    sb.Append("Удача! ");
                    switch (numFish)
                    {
                        case 1:
                            sb.Append("Поймана <font color=#008000>единственная</font> рыба, которая клевала");
                            break;
                        case 2:
                            sb.Append("Пойманы <font color=#008000>обе</font> рыбы, которые клевали");
                            break;
                        case 3:
                            sb.Append("Пойманы все <font color=#008000>три</font> рыбы, которые клевали");
                            break;
                        case 4:
                            sb.Append("Пойманы все <font color=#008000>четыре</font> рыбы, которые клевали");
                            break;
                        case 5:
                            sb.Append("Пойманы все <font color=#008000>пять</font> рыб, которые клевали");
                            break;
                    }
                }
                else
                {
                    if (numFish == 0)
                    {
                        switch (catchFish)
                        {
                            case 1:
                                sb.Append("Клевала <font color=#000080>единственная</font> рыба, но сорвалась");
                                break;
                            case 2:
                                sb.Append("Клевали <font color=#000080>две</font> рыбы, но сорвались");
                                break;
                            case 3:
                                sb.Append("Клевали <font color=#000080>три</font> рыбы, но сорвались");
                                break;
                            case 4:
                                sb.Append("Клевали <font color=#000080>четыре</font> рыбы, но сорвались");
                                break;
                            case 5:
                                sb.Append("Клевали <font color=#000080>пять</font> рыб, но сорвались");
                                break;
                        }
                    }
                    else
                    {
                        switch (numFish)
                        {
                            case 1:
                                sb.Append("Поймана <font color=#008000>одна</font> рыба");
                                break;
                            case 2:
                                sb.Append("Пойманы <font color=#008000>две</font> рыбы");
                                break;
                            case 3:
                                sb.Append("Пойманы <font color=#008000>три</font> рыбы");
                                break;
                            case 4:
                                sb.Append("Пойманы <font color=#008000>четыре</font> рыбы");
                                break;
                            case 5:
                                sb.Append("Пойманы <font color=#008000>пять</font> рыб");
                                break;
                        }

                        sb.Append(" и еще ");
                        switch (catchFish - numFish)
                        {
                            case 1:
                                sb.Append("<font color=#000080>одна</font> сорвалась");
                                break;
                            case 2:
                                sb.Append("<font color=#000080>две</font> сорвались");
                                break;
                            case 3:
                                sb.Append("<font color=#000080>три</font> сорвались");
                                break;
                            case 4:
                                sb.Append("<font color=#000080>четыре</font> сорвались");
                                break;
                        }
                    }
                }

                if (fishUmUp)
                {
                    sb.Append(@"<BR>Умение ""Рыбалка"" повысилось на 1!");
                }

                var posend = html.IndexOf(strFishReportEnd, StringComparison.OrdinalIgnoreCase);
                if (posf != -1)
                {
                    html = html.Substring(0, posf + "<br><font color=#CC0000><b>".Length) +
                           sb +
                           html.Substring(posend);
                }
            }
            else
            {
                staticFishReport = "Нет клёва.";
                posf = html.IndexOf(staticFishReport, StringComparison.OrdinalIgnoreCase);
                if (posf != -1)
                {
                    catchFish = 0;
                }
                else
                {
                    staticFishReport = "Не удалось вытащить рыбу.";
                    posf = html.IndexOf(staticFishReport, StringComparison.OrdinalIgnoreCase);
                    if (posf == -1)
                    {
                        return string.Empty;
                    }
                }
            }

            var code = 0;
            double fishp = 0;
            double fishmassa = 0;
            switch (nameFish)
            {
                case "Карась":
                    code = 0;
                    fishp = 4.32;
                    fishmassa = 2;
                    break;
                case "Плотва":
                    code = 1;
                    fishp = 3.62;
                    fishmassa = 2;
                    break;
                case "Пескарь":
                    code = 2;
                    fishp = 3.94;
                    fishmassa = 2;
                    break;
                case "Щука":
                    code = 3;
                    fishp = 23.15;
                    fishmassa = 5;
                    break;
                case "Ёрш":
                    code = 4;
                    fishp = 3.34;
                    fishmassa = 2;
                    break;
                case "Окунь":
                    code = 5;
                    fishp = 11.54;
                    fishmassa = 2;
                    break;
                case "Краснопёрка":
                    code = 6;
                    fishp = 8.58;
                    fishmassa = 2;
                    break;
                case "Налим":
                    code = 7;
                    fishp = 23.85;
                    fishmassa = 3;
                    break;
                case "Судак":
                    code = 8;
                    fishp = 13.14;
                    fishmassa = 2;
                    break;
                case "Верхоплавка":
                    code = 9;
                    fishp = 2.68;
                    fishmassa = 2;
                    break;
                case "Лещ":
                    code = 10;
                    fishp = 22.20;
                    fishmassa = 2;
                    break;
                case "Подлещик":
                    code = 11;
                    fishp = 4.76;
                    fishmassa = 2;
                    break;
                case "Карп":
                    code = 12;
                    fishp = 5.26;
                    fishmassa = 2;
                    break;
                case "Форель":
                    code = 13;
                    fishp = 29.75;
                    fishmassa = 5;
                    break;
                case "Бычок":
                    code = 14;
                    fishp = 8.80;
                    fishmassa = 2;
                    break;
                case "Голавль":
                    code = 15;
                    fishp = 7.26;
                    fishmassa = 2;
                    break;
                case "Линь":
                    code = 16;
                    fishp = 31.62;
                    fishmassa = 2;
                    break;
                case "Сом":
                    code = 17;
                    fishp = 42.04;
                    fishmassa = 4;
                    break;
                case "Язь":
                    code = 18;
                    fishp = 29.12;
                    fishmassa = 2;
                    break;
            }

            double prim = 0;
            double primassa = 0;
            var namepri = string.Empty;
            switch (AppVars.AutoFishLikeId)
            {
                case "38":
                    namepri = "Хлеб";
                    prim = 1;
                    primassa = 0.2;
                    break;
                case "39":
                    namepri = "Червяк";
                    prim = 1;
                    primassa = 0.1;
                    break;
                case "40":
                    namepri = "Крупный червяк";
                    prim = 1;
                    primassa = 0.2;
                    break;
                case "41":
                    namepri = "Опарыш";
                    prim = 5;
                    primassa = 0.1;
                    break;
                case "42":
                    namepri = "Мотыль";
                    prim = 5;
                    primassa = 0.1;
                    break;
                case "43":
                    namepri = "Блесна";
                    prim = 10;
                    primassa = 0.3;
                    break;
                case "44":
                    namepri = "Донка";
                    prim = 12;
                    primassa = 0.3;
                    break;
                case "45":
                    namepri = "Мормышка";
                    prim = 15;
                    primassa = 0.3;
                    break;
                case "46":
                    namepri = "Заговоренная блесна";
                    prim = 20;
                    primassa = 0.4;
                    break;
            }

            double bal = 0;
            var sbr = new StringBuilder();
            var s2 = new StringBuilder();
            sbr.Append("<br><table cellpadding=1 cellspacing=0 border=0 width=1%><tr><td bgcolor=#cccccc>");
            sbr.Append("<table cellpadding=0 cellspacing=0 border=0 width=1%><tr><td bgcolor=#ffffff>");
            sbr.Append("<table cellpadding=0 cellspacing=2 border=0 width=100%>");
            if (!string.IsNullOrEmpty(AppVars.AutoFishHand1))
            {
                sbr.Append("<tr><td bgcolor=#FCFAF3 nowrap class=nickname>&nbsp;");
                sbr.Append(AppVars.AutoFishHand1);
                s2.Append(AppVars.AutoFishHand1);
                sbr.Append("&nbsp;(до заброса):</td><td bgcolor=#FAFAFA nowrap class=nickname><b>&nbsp;");
                s2.Append(" (до заброса): ");
                sbr.Append(AppVars.AutoFishHand1D);
                s2.Append(AppVars.AutoFishHand1D);
                bal -= 2.5;
                sbr.Append("&nbsp;</b></td></tr>");
                s2.AppendLine();
            }

            if (!string.IsNullOrEmpty(AppVars.AutoFishHand2))
            {
                sbr.Append("<tr><td bgcolor=#FCFAF3 nowrap class=nickname>&nbsp;");
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

            if (!string.IsNullOrEmpty(AppVars.AutoFishMassa) && ((numFish > 0) || (catchFish > 0)))
            {
                sbr.Append("<tr><td bgcolor=#FCFAF3 nowrap class=nickname>&nbsp;");
                sbr.Append("Масса инвентаря");
                s2.Append("Масса инвентаря");
                sbr.Append(":</td><td bgcolor=#FAFAFA nowrap class=nickname><b>&nbsp;");
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
                        sbr.Append("&nbsp;(<font color=#CC0000>");
                        s2.Append(" (");
                        sbr.Append(sd2);
                        s2.Append(sd2);
                        sbr.Append("<img src=http://image.neverlands.ru/gameplay/down.gif width=10 height=14></font>)");
                        s2.Append(")");
                    }
                    else
                    {
                        sbr.Append("&nbsp;(<font color=#008800>+");
                        s2.Append(" (+");
                        sbr.Append(sd2);
                        s2.Append(sd2);
                        sbr.Append("<img src=http://image.neverlands.ru/gameplay/up.gif width=10 height=14></font>)");
                        s2.Append(")");
                    }
                }

                sbr.Append("&nbsp;</b></td></tr>");
                s2.AppendLine();
            }

            if (fishUmUp)
            {
                AppVars.Profile.FishUm++;
            }

            if (AppVars.Profile.FishUm > 0)
            {
                sbr.Append("<tr><td bgcolor=#FCFAF3 nowrap class=nickname>&nbsp;");
                sbr.Append("Умелка");
                s2.Append("Умелка");
                sbr.Append(":</td><td bgcolor=#FAFAFA nowrap class=nickname><b>&nbsp;");
                s2.Append(": ");
                sbr.Append(AppVars.Profile.FishUm);
                s2.Append(AppVars.Profile.FishUm);
                if (fishUmUp)
                {
                    sbr.Append("&nbsp;(<font color=#008800>+");
                    s2.Append(" (+");
                    sbr.Append(1);
                    s2.Append(1);
                    sbr.Append("<img src=http://image.neverlands.ru/gameplay/up.gif width=10 height=14></font>)");
                    s2.Append(")");
                }

                sbr.Append("</b></td></tr>");
                s2.AppendLine();
            }

            if (!string.IsNullOrEmpty(namepri) && !string.IsNullOrEmpty(AppVars.AutoFishLikeVal))
            {
                sbr.Append("<tr><td bgcolor=#FCFAF3 nowrap class=nickname>&nbsp;");
                sbr.Append(namepri);
                s2.Append(namepri);
                sbr.Append("&nbsp;(остаток):</td><td bgcolor=#FAFAFA nowrap class=nickname><b>&nbsp;");
                s2.Append(" (остаток): ");
                var cur = Convert.ToInt32(AppVars.AutoFishLikeVal, CultureInfo.InvariantCulture) - catchFish;
                sbr.Append(cur);
                s2.Append(cur);
                if (catchFish > 0)
                {
                    sbr.Append("&nbsp;(<font color=#CC0000>-");
                    s2.Append(" (-");
                    sbr.Append(catchFish);
                    s2.Append(catchFish);
                    sbr.Append("<img src=http://image.neverlands.ru/gameplay/down.gif width=10 height=14></font>)");
                    s2.Append(")");
                }

                bal -= prim * catchFish;
                sbr.Append("&nbsp;</b></td></tr>");
                s2.AppendLine();
            }

            if (!string.IsNullOrEmpty(nameFish))
            {
                sbr.Append("<tr><td bgcolor=#FCFAF3 nowrap class=nickname>&nbsp;");
                sbr.Append(nameFish);
                s2.Append(nameFish);
                sbr.Append("&nbsp;(улов):</td><td bgcolor=#FAFAFA nowrap class=nickname><b>&nbsp;");
                s2.Append(" (улов): ");
                sbr.Append(numFish);
                s2.Append(numFish);
                bal += fishp * numFish;
                sbr.Append("&nbsp;</b></td></tr>");
                s2.AppendLine();
            }

            sbr.Append("<tr><td bgcolor=#FCFAF3 nowrap class=nickname>&nbsp;");
            if (bal < 0)
            {
                sbr.Append("Приблизительные&nbsp;потери");
                s2.Append("Приблизительные потери"); 
            }
            else
            {
                sbr.Append("Приблизительный&nbsp;доход");
                s2.Append("Приблизительный доход");
            }

            sbr.Append(":</td><td bgcolor=#FAFAFA nowrap class=nickname><b>&nbsp;");
            s2.Append(": ");
            var sbal = bal.ToString("###0.##", CultureInfo.InvariantCulture);
            if (bal < 0)
            {
                sbr.Append("<font color=#CC0000>");
                sbr.Append(sbal);
                s2.Append(sbal);
                sbr.Append("<img src=http://image.neverlands.ru/gameplay/down.gif width=10 height=14></font>&nbsp;NV");
                s2.Append(" NV");
            }
            else
            {
                sbr.Append("<font color=#008800>+");
                s2.Append("+");
                sbr.Append(sbal);
                s2.Append(sbal);
                sbr.Append("<img src=http://image.neverlands.ru/gameplay/up.gif width=10 height=14></font>&nbsp;NV");
                s2.Append(" NV");
            }

            sbr.Append("&nbsp;</b></td></tr>");
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

            sbr.Append("<tr><td bgcolor=#FCFAF3 nowrap class=nickname>&nbsp;");
            if (AppVars.AutoFishNV < 0)
            {
                sbr.Append("Приблизительные&nbsp;потери&nbsp;за&nbsp;рыбалку");
                s2.Append("Приблизительные потери за рыбалку");
            }
            else
            {
                sbr.Append("Приблизительный&nbsp;доход&nbsp;за&nbsp;рыбалку");
                s2.Append("Приблизительный доход за рыбалку");
            }

            sbr.Append(":</td><td bgcolor=#FAFAFA nowrap class=nickname><b>&nbsp;");
            s2.Append(": ");
            sbal = AppVars.AutoFishNV.ToString("###0.##", CultureInfo.InvariantCulture);
            if (AppVars.AutoFishNV < 0)
            {
                sbr.Append("<font color=#CC0000>");
                sbr.Append(sbal);
                s2.Append(sbal);
                sbr.Append("<img src=http://image.neverlands.ru/gameplay/down.gif width=10 height=14></font>&nbsp;NV");
                s2.Append(" NV");
            }
            else
            {
                sbr.Append("<font color=#008800>+");
                s2.Append("+");
                sbr.Append(sbal);
                s2.Append(sbal);
                sbr.Append("<img src=http://image.neverlands.ru/gameplay/up.gif width=10 height=14></font>&nbsp;NV");
                s2.Append(" NV");
            }

            sbr.Append("&nbsp;</b></td></tr>");
            sbr.Append("</table></td></tr></table></td></tr></table>");

            var st = new StringBuilder();
            st.Append(
                "<table cellpadding=1 cellspacing=0 border=0 width=1%><tr><td bgcolor=#cccccc>" +
                "<table cellpadding=10 cellspacing=0 border=0 width=100%><tr>");
            if (numFish > 0)
            {
                st.Append("<td bgcolor=#f5f5f5 valign=middle align=center><img src=http://image.neverlands.ru/gameplay/fishing/f");
                st.Append(code);
                st.Append(@".gif width=60 height=60><br><font class=freetxt>");
                st.Append(nameFish);
                st.Append("</font></td>");
            }

            st.Append("<td bgcolor=#f5f5f5 valign=middle align=center nowrap><font class=nickname><font color=#cc0000>");
            p1 = html.IndexOf("</SCRIPT><br><font");
            html = html.Insert(p1 + "</SCRIPT><br>".Length, st.ToString());
            posf = html.IndexOf(strFishReportEnd);
            html = html.Insert(
                posf + strFishReportEnd.Length,
                sbr + @"</font></font><br><input type=button class=lbut onclick=""window.external.ShowFishTip()"" value=""Советник рыбака""></td></tr></table></td></tr></table>");

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
            return html;
        }
    }
}