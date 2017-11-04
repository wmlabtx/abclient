namespace ABClient.ABForms
{
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using AppControls;
    using System;
    using MyHelpers;
    using ExtMap;

    internal delegate void FormCompasStopDelegate();

    internal delegate void AddCharacterDelegate(string htmlCharacter);
    internal delegate void UpdateStatusDelegate(string status);

    internal sealed partial class FormCompas : Form
    {
        private readonly StringBuilder _sb = new StringBuilder();

        internal FormCompas(string nick)
        {
            /*
            for (var i = 33; i < 127; i++)
            {
                var c = Convert.ToChar(i);
                var url = string.Format("http://www.neverlands.ru/ch.php?lo=1&r={0}_1000_1000", c);
                try
                {
                    var httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
                    httpWebRequest.Method = "GET";
                    httpWebRequest.Proxy = AppVars.LocalProxy;
                    var cookies = CookiesManager.Obtain("www.neverlands.ru");
                    httpWebRequest.Headers.Add("Cookie", cookies);
                    var resp = httpWebRequest.GetResponse();
                    var webstream = resp.GetResponseStream();
                    var reader = new StreamReader(webstream, AppVars.Codepage);
                    var responseFromServer = reader.ReadToEnd();
                    //if (responseFromServer.IndexOf("Умник", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        var file = string.Format("compas{0}.txt", i);
                        File.WriteAllText(file, responseFromServer);
                    }
                }
                catch
                {
                }
            }
            */

            // "черный:Черный:16:nona.gif;LightSoulS;Бакинский комисар:0:0:0:0:lights.gif;Дети Света"

            InitializeComponent();
            wb.BeforeNavigate += WbBeforeNavigate;
            _sb.Append(
                "<html>" +
                "<head>" +
                @"<meta content=""text/html; charset=windows-1251"" http-equiv=Content-type>" +
                "<META Http-Equiv=Cache-Control Content=no-cache>" +
                "<body bgcolor=#FCFAF3>");
            UpdateReport();
            ProcessNick(nick);
        }

        static void WbBeforeNavigate(object sender, WebBrowserExtendedNavigatingEventArgs e)
        {
            var url = e.Address;
            if (!url.StartsWith("http://")) return;
            if (url.IndexOf("pinfo.cgi?", StringComparison.Ordinal) != -1)
            {
                    
                if (AppVars.MainForm != null)
                {                        
                    AppVars.MainForm.BeforeNewWindow(url);
                }
            }
            else
            {
                var regnum = url.Substring("http://".Length);
                if (regnum.EndsWith("/"))
                {
                    regnum = regnum.Substring(0, regnum.Length - 1);
                    if (AppVars.MainForm != null)
                    {
                        e.Cancel = true;
                        AppVars.MainForm.MoveToDialog(regnum);
                    }                    
                }
            }

            e.Cancel = true;
        }

        internal static void SetNick(string nick)
        {
            ProcessNick(nick);
        }

        private void UpdateStatus(string message)
        {
            status.Text = message;
        }

        private void AddCharacter(string htmlCharacter)
        {
            _sb.Append(htmlCharacter);
            UpdateReport();
        }

        private void UpdateReport()
        {
            wb.DocumentText = _sb + "</body></html>";
        }

        private static void ProcessNick(string nick)
        {
            ThreadPool.QueueUserWorkItem(CompasAsync, nick);
        }

        private void FormCompas_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppVars.VipFormCompas = null;
        }

        private static void CompasAsync(object stateInfo)
        {
            var nick = stateInfo as string;
            if (nick == null)
            {
                return;
            }

            //var wc = new WebClient { Proxy = AppVars.LocalProxy };
            var html = NeverInfo.GetPInfo(nick);
            if (string.IsNullOrEmpty(html))
                return;

            // 2/11/2017 - params -> parameters 
            // var params0 = HelperStrings.SubString(html, "var params = [[", "],");
            var params0 = HelperStrings.SubString(html, "var parameters = [[", "],");
            if (string.IsNullOrEmpty(params0))
            {
                return;
            }

            var spar0 = HelperStrings.ParseArguments(params0);
            if (spar0.Length < 9)
            {
                return;
            }

            var firstNick = spar0[0].Trim();
            var firstSign = spar0[2];
            var firstClan = spar0[8];

            var listClanNicks = new List<string> { firstNick };
            if (!string.IsNullOrEmpty(firstClan))
            {
                byte[] buffer;
                using (var wc = new WebClient { Proxy = AppVars.LocalProxy })
                {
                    try
                    {
                        IdleManager.AddActivity();
                        buffer = wc.DownloadData(new Uri("http://neverok.ru/clan_players.php?type=all"));
                    }
                    catch (WebException)
                    {
                        return;
                    }
                    finally
                    {
                        IdleManager.RemoveActivity();
                    }
                }

                html = AppVars.Codepage.GetString(buffer);
                try
                {
                    if (AppVars.VipFormCompas != null)
                    {
                        var message = string.Format($"Получаем список клана [{firstClan}]...");
                        AppVars.VipFormCompas.BeginInvoke(
                            new UpdateStatusFormAddClanDelegate(AppVars.VipFormCompas.UpdateStatus), message);
                    }
                    else
                    {
                        return;
                    }
                }
                catch (InvalidOperationException)
                {
                }

                var linkClan = HelperStrings.SubString(html, firstSign + @"""><a href=""clan_players.php?clantype=", @"""");
                if (string.IsNullOrEmpty(linkClan))
                    return;

                using (var wc = new WebClient { Proxy = AppVars.LocalProxy })
                {
                    try
                    {
                        IdleManager.AddActivity();
                        buffer = wc.DownloadData(new Uri($"http://neverok.ru/clan_players.php?clantype={linkClan}"));
                    }
                    catch (WebException)
                    {
                        return;
                    }
                    finally
                    {
                        IdleManager.RemoveActivity();
                    }
                }

                html = AppVars.Codepage.GetString(buffer);
                if (string.IsNullOrEmpty(html))
                    return;

                var p1 = 0;
                while (p1 != -1)
                {
                    const string pat1 = @" target=""_blank"">";
                    p1 = html.IndexOf(pat1, p1, StringComparison.OrdinalIgnoreCase);
                    if (p1 == -1)
                        break;

                    p1 += pat1.Length;
                    var p2 = html.IndexOf(@"[", p1, StringComparison.OrdinalIgnoreCase);
                    if (p2 == -1)
                        continue;

                    var statnick = html.Substring(p1, p2 - p1);
                    if (statnick.Length < 64)
                        listClanNicks.Add(statnick);
                }
            }

            foreach (var vipNick in listClanNicks)
            {
                var firstMessage = $"Анализируем [{vipNick}]";
                try
                {
                    if (AppVars.VipFormCompas != null)
                    {
                        AppVars.VipFormCompas.BeginInvoke(
                            new UpdateStatusDelegate(AppVars.VipFormCompas.UpdateStatus), firstMessage);
                    }
                    else
                    {
                        return;
                    }
                }
                catch (InvalidOperationException)
                {
                }

                html = NeverInfo.GetPInfo(vipNick);
                if (string.IsNullOrEmpty(html))
                    continue;

                // 2/11/2017 - params -> parameters 
                // var params0 = HelperStrings.SubString(html, "var params = [[", "],");
                params0 = HelperStrings.SubString(html, "var parameters = [[", "],");
                if (string.IsNullOrEmpty(params0))
                {
                    continue;
                }

                spar0 = HelperStrings.ParseArguments(params0);
                if (spar0.Length < 9)
                {
                    continue;
                }

                var infNick = spar0[0].Trim();
                var infAlign = spar0[1];
                var infSign = spar0[2];
                var infClan = spar0[8];
                var infLevel = spar0[3];
                var orgLocation = spar0[5];
                var infLocation = spar0[5];
                if (infLocation.IndexOf('[') != -1)
                {
                    infLocation = HelperStrings.SubString(infLocation, "[", "]");
                }

                var infIsOnline = !string.IsNullOrEmpty(infLocation);

                if (!infClan.Equals(firstClan))
                {
                    continue;
                }

                var isLightWound = false;
                var isMediumWound = false;
                var isHeavyWound = false;
                var isUltimateWound = false;

                var effects = HelperStrings.SubString(html, "var effects = [", "];");
                var sbeff = new StringBuilder();
                if (!string.IsNullOrEmpty(effects))
                {
                    var seffects = effects.Split(new[] { "],[" }, StringSplitOptions.RemoveEmptyEntries);
                    for (var k = 0; k < seffects.Length; k++)
                    {
                        var effk = seffects[k].Trim(new[] { '[', ']' });
                        var seffk = effk.Split(',');
                        if (seffk.Length <= 1)
                        {
                            continue;
                        }

                        var effcode = seffk[0];
                        var effname = seffk[1].Replace("<b>", String.Empty).Replace("</b>", String.Empty);
                        sbeff.AppendFormat(
                            @"&nbsp;<img src=http://image.neverlands.ru/pinfo/eff_{0}.gif width=15 height=15 align=absmiddle title=""{1}"">",
                            effcode,
                            effname);
                        switch (effcode)
                        {
                            case "1":
                                isUltimateWound = true;
                                break;

                            case "2":
                                isHeavyWound = true;
                                break;

                            case "3":
                                isMediumWound = true;
                                break;

                            case "4":
                                isLightWound = true;
                                break;
                        }
                    }
                }

                var travma = string.Empty;
                if (isUltimateWound)
                {
                    travma = "боевая";
                }
                else
                {
                    if (isHeavyWound)
                    {
                        travma = "тяжелая";
                    }
                    else
                    {
                        if (isMediumWound)
                        {
                            travma = "средняя";
                        }
                        else
                        {
                            if (isLightWound)
                            {
                                travma = "легкая";
                            }
                        }
                    }
                }

                var colorNick = infNick;
                if (!infIsOnline)
                {
                    colorNick = @"<font color=""#cccccc"">" + colorNick + "</font>";
                }
                else
                {
                    if (!string.IsNullOrEmpty(travma))
                    {
                        switch (travma)
                        {
                            case "боевая":
                                colorNick = @"<font color=""#666600"">" + colorNick + "</font>";
                                break;
                            case "тяжелая":
                                colorNick = @"<font color=""#c10000"">" + colorNick + "</font>";
                                break;
                            case "средняя":
                                colorNick = @"<font color=""#e94c69"">" + colorNick + "</font>";
                                break;
                            case "легкая":
                                colorNick = @"<font color=""#ef7f94"">" + colorNick + "</font>";
                                break;
                        }
                    }
                }

                var align = string.Empty;
                var ali1 = string.Empty;
                var ali2 = string.Empty;
                switch (infAlign)
                {
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

                if (!string.IsNullOrEmpty(ali1))
                {
                    align =
                        "<img src=http://image.neverlands.ru/signs/" +
                        ali1 +
                        @" width=15 height=12 align=absmiddle border=0 title=""" +
                        ali2 +
                        @""">&nbsp";
                }

                var ss = string.Empty;
                if (!string.IsNullOrEmpty(infClan))
                {
                    ss =
                        "<img src=http://image.neverlands.ru/signs/" +
                        infSign +
                        @" width=15 height=12 align=absmiddle title=""" +
                        infClan +
                        @""">&nbsp;";
                }

                var location = string.Empty;
                if (infIsOnline)
                {
                    // Окрестности Форпоста [Окрестность Форпоста, Биржа]"

                    if (!orgLocation.StartsWith("Форпост", StringComparison.OrdinalIgnoreCase) &&
                        !orgLocation.StartsWith("Октал", StringComparison.OrdinalIgnoreCase) &&
                        !orgLocation.StartsWith("Деревня", StringComparison.OrdinalIgnoreCase) &&
                        !orgLocation.StartsWith("Тюрьма", StringComparison.OrdinalIgnoreCase) &&
                        !orgLocation.StartsWith("Окрестности Форпоста [Окрестность Форпоста, Биржа]", StringComparison.OrdinalIgnoreCase) &&
                        !orgLocation.StartsWith("Окрестность Форпоста [Сырьевая Биржа]", StringComparison.OrdinalIgnoreCase))                      
                    {
                        //var matrixNick = string.Format(":{0}:", vipNick);
                        var arrayPossibleLocations = new List<string>();
                        var cellCount = 0;
                        foreach (var cellKey in Map.Cells.Keys)
                        {
                            var cell = Map.Cells[cellKey];
                            if (!cell.Tooltip.Equals(infLocation, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            cellCount++;
                        }

                        if (cellCount > 0)
                        {
                            var cellIndex = 0;
                            foreach (var cellKey in Map.Cells.Keys)
                            {
                                var cell = Map.Cells[cellKey];
                                if (!cell.Tooltip.Equals(infLocation, StringComparison.OrdinalIgnoreCase))
                                {
                                    continue;
                                }

                                var progress = (cellIndex*100)/cellCount;
                                var status = $"{firstMessage}... {progress}%";
                                try
                                {
                                    if (AppVars.VipFormCompas != null)
                                    {
                                        AppVars.VipFormCompas.BeginInvoke(
                                            new UpdateStatusDelegate(AppVars.VipFormCompas.UpdateStatus),
                                            new object[] { status });
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                catch (InvalidOperationException)
                                {
                                }

                                cellIndex++;

                                arrayPossibleLocations.Add(cellKey);
                                /*
                                var position = Map.InvPosition[cellKey];
                                var cm = Convert.ToChar(38);
                                var url = string.Format("http://www.neverlands.ru/ch.php?lo=1&r={0}_{1}_{2}", cm, position.X, position.Y);
                                try
                                {
                                    var httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
                                    httpWebRequest.Method = "GET";
                                    httpWebRequest.Proxy = AppVars.LocalProxy;
                                    var cookies = CookiesManager.Obtain("www.neverlands.ru");
                                    httpWebRequest.Headers.Add("Cookie", cookies);
                                    var resp = httpWebRequest.GetResponse();
                                    var webstream = resp.GetResponseStream();
                                    var reader = new StreamReader(webstream, AppVars.Codepage);
                                    var responseFromServer = reader.ReadToEnd();
                                    if (responseFromServer.IndexOf(matrixNick, StringComparison.OrdinalIgnoreCase) != -1)
                                    {
                                        arrayPossibleLocations.Clear();
                                        arrayPossibleLocations.Add(cellKey);
                                        break;
                                    }
                                }
                                catch
                                {
                                }
                                 */ 
                            }
                        }

                        if (arrayPossibleLocations.Count > 0)
                        {
                            var possibleLocations = arrayPossibleLocations.Count;
                            var path = new MapPath(AppVars.LocationReal, arrayPossibleLocations.ToArray());
                            if (possibleLocations == 1)
                            {
                                location =
                                    $"Точное положение: <a style='text-decoration:none' href='http://{arrayPossibleLocations[0]}'><b>{arrayPossibleLocations[0]}</b></a> (шагов: <b>{path.Jumps}</b>). {infLocation}.";
                            }
                            else
                            {
                                location =
                                    $"{infLocation}. Возможных клеток: <b>{possibleLocations}</b>, ближайшая <a style='text-decoration:none' href='http://{path.Destination ?? AppVars.LocationReal}'><b>{path.Destination ?? AppVars.LocationReal}</b></a> (шагов: <b>{path.Jumps}</b>).";
                            }
                        }
                        else
                        {
                            location = @"<font color=""#cc0000"">" + "Неизвестная клетка!" + "</font>";
                        }
                    }
                    else
                    {
                        string[] dest = null;
                        if (orgLocation.StartsWith("Форпост") || orgLocation.StartsWith("Тюрьма"))
                        {
                            dest = new[] { "8-259", "8-294" };
                        }

                        if (orgLocation.StartsWith("Октал"))
                        {
                            dest = new[] { "12-428", "12-494", "12-521" };
                        }

                        if (orgLocation.StartsWith("Деревня"))
                        {
                            dest = new[] { "8-197", "8-228", "8-229" };
                        }

                        if (orgLocation.StartsWith("Окрестности Форпоста [Окрестность Форпоста, Биржа]"))
                        {
                            dest = new[] { "8-227" };
                        }

                        if (orgLocation.StartsWith("Окрестность Форпоста [Сырьевая Биржа]"))
                        {
                            dest = new[] { "8-227" };
                        }
                        
                        if (dest != null)
                        {
                            var path = new MapPath(AppVars.LocationReal, dest);
                            location = $"Точное положение: <a style='text-decoration:none' href='http://{path.Destination}'><b>{path.Destination}</b></a> (шагов: <b>{path.Jumps}</b>). {infLocation}.";
                        }
                    }
                }
                else
                {
                    location = @"<font color=""#cccccc"">" + "В оффлайне или невиде." + "</font>";
                }

                location = @"<span style=""font-size:10px;"">" + location + "</span>";
                var result =
                    @"<span style=""font-family:Verdana,Arial,sans-serif; font-size:12px; text-decoration:none;	color:#222222;"">" +
                    align +
                    ss +
                    @"<b>" +
                    colorNick +
                    "</b></a>[" +
                    infLevel +
                    @"]<a href='http://www.neverlands.ru/pinfo.cgi?" + infNick + "'><img src=http://image.neverlands.ru/chat/info.gif width=11 height=12 border=0 align=absmiddle></a>" +
                    sbeff +
                    "<img src=http://image.neverlands.ru/1x1.gif width=8 height=1>" +
                    location +
                    "</span><br>";

                try
                {
                    if (AppVars.VipFormCompas != null)
                    {
                        AppVars.VipFormCompas.BeginInvoke(
                            new AddCharacterDelegate(AppVars.VipFormCompas.AddCharacter), result);
                    }
                    else
                    {
                        return;
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }

            try
            {
                if (AppVars.VipFormCompas != null)
                {
                    AppVars.VipFormCompas.BeginInvoke(
                        new UpdateStatusDelegate(AppVars.VipFormCompas.UpdateStatus), "Сканирование завешено.");
                }
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}
