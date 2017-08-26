using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ABClient.ABForms;
using ABClient.ABProxy;
using ABClient.MyHelpers;
using ABClient.MySounds;
using ABClient.Helpers;

namespace ABClient
{
    internal static class RoomManager
    {
        private static Thread _thread;
        private static readonly AutoResetEvent Event = new AutoResetEvent(false);
        private static bool _doStop;
        private static string _oldRoom = string.Empty;
        private static readonly SortedList<string, DateTime> BlackList = new SortedList<string, DateTime>();

        internal static List<string> MyLocation = new List<string>();

        internal static void StartTracing()
        {
            if (_thread != null)
                return;

            _thread = new Thread(RoomAsync)
            {
                Name = "RoomAsync"
            };

            _thread.Start();            
        }

        internal static void StopTracing()
        {
            if (_thread == null)
                return;

            _doStop = true;
            Event.Set();
            while (_thread.IsAlive)
            {
                _thread.Join(50);
            }

            _thread = null;
        }

        internal static void CharAddToBlackList(string nick)
        {
            var key = nick.ToLower();
            if (!BlackList.ContainsKey(key))
            {
                BlackList.Add(key, DateTime.Now);
            }
        }

        private static bool IsCharInBlackList(string nick)
        {
            var key = nick.ToLower();
            if (!BlackList.ContainsKey(key))
            {
                return false;
            }

            if (DateTime.Now.Subtract(BlackList[key]).TotalSeconds > 10)
            {
                BlackList.Remove(key);
                return false;
            }

            return true;
        }

        private static void RoomAsync(object stateInfo)
        {
            while (!_doStop)
            {
                if (AppVars.DoShowWalkers)
                {
                    try
                    {
                        IdleManager.AddActivity();
                        var wr = (HttpWebRequest) WebRequest.Create("http://neverlands.ru/ch.php?lo=1&");
                        wr.Method = "GET";
                        wr.Proxy = AppVars.LocalProxy;
                        var cookies = CookiesManager.Obtain("www.neverlands.ru");
                        wr.Headers.Add("Cookie", cookies);
                        var resp = wr.GetResponse();
                        var webstream = resp.GetResponseStream();
                        if (webstream != null)
                        {
                            using (var reader = new StreamReader(webstream, AppVars.Codepage))
                            {
                                var responseFromServer = reader.ReadToEnd();
                                {
                                    var ssum = HelperStrings.SubString(responseFromServer, "</b></font></a> [ ", " ]");
                                    var slist = HelperStrings.SubString(responseFromServer, "var ChatListU = new Array(",
                                        ");");
                                    var newRoom = $"[{ssum}] {slist}";
                                    if (string.Compare(newRoom, _oldRoom, StringComparison.Ordinal) != 0)
                                    {
                                        Process(responseFromServer);
                                        _oldRoom = newRoom;

                                        try
                                        {
                                            if (AppVars.MainForm != null)
                                            {
                                                AppVars.MainForm.BeginInvoke(
                                                    new ReloadChPhpInvokeDelegate(AppVars.MainForm.ReloadChPhpInvoke),
                                                    new object[] {});
                                            }
                                        }
                                        catch (InvalidOperationException)
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (WebException)
                    {
                    }
                    catch (IOException)
                    {
                    }
                    finally
                    {
                        IdleManager.RemoveActivity();
                    }

                }

                Event.WaitOne(100, false);
            }
        }

        private class FilterProcRoomResult
        {
            internal int NumCharsInRoom;
            internal string EnemyAttack;
        }

        internal static string Process(string html)
        {
            var description = HelperStrings.SubString(html, "<font class=placename><b>", "</b></font>");
            if (description != null)
            {
                description = description.Replace("<br>", " ");
                AppVars.LocationName = description;
            }

            var resultFilterProcRoom = FilterProcRoom(html);
            FilterGetWalkers(html);
            html = html.Replace(
                "</HEAD>",
                @"<style type=""text/css"">" +
                                @"a.activenick { background-color:inherit; padding: 2 2 2 2; }" +
                                @"a.activenick:hover { background-color:#99CCFF; padding: 2 2 2 2; }" +
                                @"a.activeico { background-color:inherit; padding: 1 1 1 1; }" +
                                @"a.activeico:hover { background-color:#FF9933; padding: 1 1 1 1; }" +
                                @"</style>" +
                                @"</head>");

            var pos = html.IndexOf("<font", StringComparison.InvariantCultureIgnoreCase);
            if (pos != -1)
            {
                html = html.Insert(pos,
                                   @"<script Language=""JavaScript"">" +
                                   @"function navto()" +
                                   @"{" +
                                   @"e_m = get_by_id ('navbox');" +
                                   @"location = e_m.value;" +
                                   @"}" +
                                   @"function get_by_id(name)" +
                                   @"{" +
                                   @"if (document.getElementById) return document.getElementById(name);" +
                                   @"else if (document.all) return document.all[name];" +
                                   @"}" +
                                   @"</script>" +
                                   @"<select id=""navbox"" onchange=""navto()"" style=""font-family:Arial; font-size:8pt"">" +
                                   @"<option value=""ch.php?lo=1&"">Текущая клетка</option>" +
                                   @"<option value=""ch.php?lo=1&"">Исходная клетка</option>" +
                                   @"<option value=""ch.php?lo=1&r=arena0"">Зал Помощи</option>" +
                                   @"<option value=""ch.php?lo=1&r=arena1"">Тренировочный зал</option>" +
                                   @"<option value=""ch.php?lo=1&r=arena2"">Зал Испытаний</option>" +
                                   @"<option value=""ch.php?lo=1&r=arena3"">Зал Посвящения</option>" +
                                   @"<option value=""ch.php?lo=1&r=arena4"">Зал Покровителей</option>" +
                                   @"<option value=""ch.php?lo=1&r=arena5"">Зал Закона</option>" +
                                   @"<option value=""ch.php?lo=1&r=main"">Городская площадь</option>" +
                                   @"<option value=""ch.php?lo=1&r=shop_1"">Лавка</option>" +
                                   @"<option value=""ch.php?lo=1&r=workshop"">Мастерская</option>" +
                                   @"<option value=""ch.php?lo=1&r=bar0"">Таверна, Большой Зал</option>" +
                                   @"<option value=""ch.php?lo=1&r=hospi"">Больница</option>" +
                                   @"<option value=""ch.php?lo=1&r=hospi1"">Комната отдыха</option>" +
                                   @"<option value=""ch.php?lo=1&r=hospi2"">Палата</option>" +
                                   @"<option value=""ch.php?lo=1&r=hpr"">Магазин подарков</option>" +
                                   @"<option value=""ch.php?lo=1&r=hdi"">Дом дилеров</option>" +
                                   @"<option value=""ch.php?lo=1&r=hau"">Аукцион</option>" +
                                   @"<option value=""ch.php?lo=1&r=hba"">Банк</option>" +
                                   @"<option value=""ch.php?lo=1&r=obe"">Обелиск</option>" +
                                   @"<option value=""ch.php?lo=1&r=post"">Почтовая служба</option>" +
                                   @"<option value=""ch.php?lo=1&r=market"">Рынок</option>" +
                                   @"<option value=""ch.php?lo=1&r=prison"">Тюрьма</option>" +
                                   @"<option value=""ch.php?lo=1&r=shop_2"">Деревня:Лавка</option>" +
                                   @"<option value=""ch.php?lo=1&r=arena20"">Деревня:Арена</option>" +
                                   @"<option value=""ch.php?lo=1&r=hsp_1"">Октал:Больница</option>" +
                                   @"<option value=""ch.php?lo=1&r=shop_3"">Октал:Лавка</option>" +
                                   @"<option value=""ch.php?lo=1&r=rem_1"">Октал:Пункт переработки</option>" +
                                   @"</select>" +
                                   @"<br>");
            }

            if (AppVars.DoShowWalkers)
            {
                html = html.Replace(
                    "<body bgcolor=#FCFAF3",
                    "<body bgcolor=#ECEAE3");
            }


            int sum;
            var ssum = HelperStrings.SubString(html, "</b></font></a> [ ", " ]");
            if (int.TryParse(ssum, out sum))
            {
                var numnev =  sum - resultFilterProcRoom.NumCharsInRoom;
                AppVars.MyNevids = numnev;
                if (numnev > 0)
                {
                    var posv = html.IndexOf("<b>Всего</b>", StringComparison.Ordinal);
                    if (posv != -1)
                    {
                        html =
                            html.Substring(0, posv) +
                            @"<b><font color=#2d5063>Невидимок</font></b> [ " +
                            numnev.ToString(CultureInfo.InvariantCulture) +
                            " ]  <a class=\"activeico\" href=\"javascript:window.external.FastAttackOpenNevid()\"><img src=http://image.neverlands.ru/weapon/i_w28_28.gif width=14 height=11 border=0 alt='Свиток Обнаружения' align=absmiddle></a><br>" +
                            html.Substring(posv);
                        html = html.Replace(
                            "<body bgcolor=#FCFAF3",
                            "<body bgcolor=#F3FAFC");
                    }

                    if (AppVars.AutoOpenNevid && !AppVars.FastNeed)
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.WriteChatMsgSafe("Пытаемся резвеять <font color=#5D7C91><b>невидимку</b></font>!");
                            AppVars.MainForm.FastAttackOpenNevid();
                            return html;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(resultFilterProcRoom.EnemyAttack) && (AppVars.AutoAttackToolId != 0))
                {
                    if (!AppVars.FastNeed)
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.WriteChatMsgSafe(string.Format("Пытаемся напасть на <b>{0}</b>!", resultFilterProcRoom.EnemyAttack));
                            int toolid = ContactsManager.GetToolIdOfContact(resultFilterProcRoom.EnemyAttack);
                            if (toolid == 0)
                            {
                                toolid = AppVars.AutoAttackToolId;
                            }

                            switch (toolid)
                            {
                                case 1:
                                    FormMain.FastAttackUltimateAutoAttack(resultFilterProcRoom.EnemyAttack);
                                    break;
                                case 2:
                                    FormMain.FastAttackClosedUltimateAutoAttack(resultFilterProcRoom.EnemyAttack);
                                    break;
                                case 3:
                                    FormMain.FastAttackFistAutoAttack(resultFilterProcRoom.EnemyAttack);
                                    break;
                                case 4:
                                    FormMain.FastAttackClosedFistAutoAttack(resultFilterProcRoom.EnemyAttack);
                                    break;
                                case 5:
                                    FormMain.FastAttackPortalAutoAttack(resultFilterProcRoom.EnemyAttack);
                                    break;
                            }

                            return html;
                        }
                    }
                }
            }

            return html;
        }

        private static FilterProcRoomResult FilterProcRoom(string html)
        {
            var resultFilterProcRoom = new FilterProcRoomResult();
            var arg = HelperStrings.SubString(html, "new Array(", ");");
            if (string.IsNullOrEmpty(arg))
            {
                return resultFilterProcRoom;
            }

            var par = arg.Split(new[] { @"""," }, StringSplitOptions.RemoveEmptyEntries);
            if (par.Length == 0)
            {
                return resultFilterProcRoom;
            }

            var pvlist = new List<ToolStripMenuItem>();
            var trlist = new List<ToolStripMenuItem>();
            var sbtx = new StringBuilder();
            var sbtt = new StringBuilder[4];
            for (var i = 0; i < 4; i++)
            {
                sbtt[i] = new StringBuilder();
            }

            var enemyAttack = new List<string>();

            var sbt = new StringBuilder();
            var nt = new int[4];

            MyLocation.Clear();

            for (var i = 0; i < par.Length; i++)
            {
                // "виннипух:винниПУх:16:myst.gif;Mystery Orden;нервоиспытатель:0:0:0:0:sumers.gif;Дети Сумерек",
                // "revolted:Revolted:16:myst.gif;Mystery Orden;Со вступом:0:0:0:0:sumers.gif;Дети Сумерек",

                var pars = par[i].Split(':');
                if (pars.Length < 8)
                {
                    continue;
                }

                // Сохранение данных об игроке
                var userName = pars[1].Replace("<i>", string.Empty).Replace("</i>", string.Empty);
                var userLevel = pars[2];
                var userSign = string.Empty;
                var userStatus = string.Empty;
                var userSklSign = string.Empty;
                var userSklStatus = string.Empty;
                if (!string.IsNullOrEmpty(pars[3]))
                {
                    var splpar = pars[3].Split(';');
                    if (splpar.Length == 3)
                    {
                        userSign = splpar[0];
                        userStatus = splpar[1] + ", " + splpar[2];
                    }
                }

                if ((pars.Length > 8) && !string.IsNullOrEmpty(pars[8]))
                {
                    var splpar = pars[8].Split(';');
                    if (splpar.Length == 2)
                    {
                        userSklSign = splpar[0];
                        userSklStatus = splpar[1];
                    }
                }

                MyLocation.Add(userName);

                if (AppVars.DoSelfNevid && !AppVars.SelfNevidNeed && pars[1].Equals(AppVars.Profile.UserNick, StringComparison.CurrentCultureIgnoreCase))
                {
                    AppVars.SelfNevidNeed = true;
                    AppVars.SelfNevidSkl = userSklStatus;
                    AppVars.SelfNevidStage = 0;
                    FormMain.ReloadMainFrame();
                }

                if (AppVars.Profile.DoChatLevels)
                    ChatUsersManager.AddUser(new ChatUser(userName, userLevel, userSign, userStatus));

                if (ContactsManager.GetClassIdOfContact(userName) == 1)
                    enemyAttack.Add(userName);

                if (pars[3].StartsWith("pv", StringComparison.OrdinalIgnoreCase))
                {
                    var pos = pars[3].LastIndexOf(';');
                    if (pos == -1)
                    {
                        continue;
                    }

                    var tsmi = new ToolStripMenuItem(userName)
                    {
                        Image = Properties.Resources._16x16_private,
                        ToolTipText = pars[3].Substring(pos + 1),
                        AutoToolTip = true
                    };
                    if (AppVars.MainForm != null)
                    {
                        tsmi.Click += AppVars.MainForm.OnPvFastToolStripMenuItemClick;
                    }

                    pvlist.Add(tsmi);
                }

                if (string.IsNullOrEmpty(pars[6]) || pars[6].Equals("0", StringComparison.Ordinal))
                    continue;

                var ntr = new int[4];
                var str = new[] { "легкая", "средняя", "тяжелая", "боевая" };
                var travmtime = new int[4];
                var tr = pars[6].Split(new []{", "}, StringSplitOptions.RemoveEmptyEntries);
                for (var t = 0; t < tr.Length; t++)
                {
                    var ttt = tr[t];
                    for (var j = 0; j < 4; j++)
                    {
                        if (!AppVars.Profile.CureEnabled[j])
                        {
                            continue;
                        }

                        if (ttt.IndexOf(str[j], StringComparison.OrdinalIgnoreCase) == -1)
                        {
                            continue;
                        }

                        int ttime;
                        int min;
                        int hour;
                        var tan = ttt.Split(' ');
                        switch (tan.Length)
                        {
                            case 4:
                                if (!int.TryParse(tan[2], out min))
                                {
                                    min = 0;
                                }

                                if (tan[3].Equals("ч", StringComparison.OrdinalIgnoreCase))
                                {
                                    ttime = min * 60;
                                }
                                else
                                {
                                    ttime = min;
                                }

                                break;
                            case 5:
                                if (!int.TryParse(tan[3], out min))
                                {
                                    min = 0;
                                }

                                if (tan[4].Equals("ч", StringComparison.OrdinalIgnoreCase))
                                {
                                    ttime = min * 60;
                                }
                                else
                                {
                                    ttime = min;
                                }

                                break;
                            case 6:
                                if (!int.TryParse(tan[4], out min))
                                {
                                    min = 0;
                                }

                                if (tan[5].Equals("ч", StringComparison.OrdinalIgnoreCase))
                                {
                                    ttime = min * 60;
                                }
                                else
                                {
                                    if (!int.TryParse(tan[2], out hour))
                                    {
                                        hour = 0;
                                    }

                                    ttime = (hour * 60) + min;
                                }

                                break;
                            case 7:
                                if (!int.TryParse(tan[3], out hour))
                                {
                                    hour = 0;
                                }

                                if (!int.TryParse(tan[5], out min))
                                {
                                    min = 0;
                                }

                                ttime = (hour * 60) + min;
                                break;
                            case 8:
                                if (!int.TryParse(tan[4], out hour))
                                {
                                    hour = 0;
                                }

                                if (!int.TryParse(tan[6], out min))
                                {
                                    min = 0;
                                }

                                ttime = (hour * 60) + min;
                                break;
                            default:
                                ttime = 0;
                                break;
                        }

                        if (travmtime[j] < ttime)
                        {
                            travmtime[j] = ttime;
                        }

                        ntr[j]++;
                    }
                }

                if ((ntr[0] + ntr[1] + ntr[2] + ntr[3]) <= 0) continue;
                var sb = new StringBuilder();
                sb.Append(userName);
                sb.Append(" [");
                sb.Append(userLevel);
                sb.Append("]: ");
                if (ntr[0] > 0 && ((ntr[1] + ntr[2] + ntr[3]) == 0))
                {
                    if (ntr[0] == 1)
                    {
                        sb.Append("легкая");
                    }
                    else
                    {
                        sb.Append(ntr[0]);
                        sb.Append(" легких");
                    }

                    sb.Append(' ');
                    sb.Append(HelperConverters.MinsToStr(travmtime[0]));
                }
                else
                {
                    if (ntr[1] > 0 && ((ntr[0] + ntr[2] + ntr[3]) == 0))
                    {
                        if (ntr[1] == 1)
                        {
                            sb.Append("средняя");
                        }
                        else
                        {
                            sb.Append(ntr[1]);
                            sb.Append(" средних");
                        }

                        sb.Append(' ');
                        sb.Append(HelperConverters.MinsToStr(travmtime[1]));
                    }
                    else
                    {
                        if (ntr[2] > 0 && ((ntr[0] + ntr[1] + ntr[3]) == 0))
                        {
                            if (ntr[2] == 1)
                            {
                                sb.Append("тяжелая");
                            }
                            else
                            {
                                sb.Append(ntr[2]);
                                sb.Append(" тяжелых");
                            }

                            sb.Append(' ');
                            sb.Append(HelperConverters.MinsToStr(travmtime[2]));
                        }
                        else
                        {
                            if (ntr[3] > 0 && ((ntr[0] + ntr[1] + ntr[2]) == 0))
                            {
                                if (ntr[3] == 1)
                                {
                                    sb.Append("боевая");
                                }
                                else
                                {
                                    sb.Append(ntr[3]);
                                    sb.Append(" боевых");
                                }

                                sb.Append(' ');
                                sb.Append(HelperConverters.MinsToStr(travmtime[3]));
                            }
                            else
                            {
                                for (var j = 0; j < 4; j++)
                                {
                                    if (ntr[j] == 0)
                                    {
                                        sb.Append('-');
                                    }
                                    else
                                    {
                                        sb.Append(ntr[j]);
                                    }

                                    if (j < 3)
                                    {
                                        sb.Append('/');
                                    }
                                }

                                var travmmax = 0;
                                for (var j = 3; j >= 0; j--)
                                {
                                    if (travmtime[j] == 0)
                                    {
                                        continue;
                                    }

                                    travmmax = travmtime[j];
                                    break;
                                }

                                sb.Append(' ');
                                sb.Append(HelperConverters.MinsToStr(travmmax));
                            }
                        }
                    }
                }

                if (sbtx.Length > 0)
                {
                    sbtx.Append(':');
                }

                sbtx.Append(userName);
                var trmi = new ToolStripMenuItem(sb.ToString())
                {
                    Image = (ntr[3] > 0
                                 ? Properties.Resources._15x12_tr4
                                 : (ntr[2] > 0
                                        ? Properties.Resources._15x12_tr3
                                        : (ntr[1] > 0
                                               ? Properties.Resources._15x12_tr2
                                               : Properties.Resources._15x12_tr1))),
                    ImageScaling = ToolStripItemImageScaling.None
                };

                if (AppVars.Profile.CureDisabledLowLevels &&
                    (pars[2].Equals("0", StringComparison.Ordinal) ||
                      pars[2].Equals("1", StringComparison.Ordinal) ||
                      pars[2].Equals("2", StringComparison.Ordinal) ||
                      pars[2].Equals("3", StringComparison.Ordinal) ||
                      pars[2].Equals("4", StringComparison.Ordinal)))
                {
                    trmi.Enabled = false;
                }
                else
                {
                    var travmtype = -1;
                    if (ntr[3] > 0)
                    {
                        nt[3]++;
                        travmtype = 3;
                    }
                    else
                    {
                        if (ntr[2] > 0)
                        {
                            nt[2]++;
                            if (travmtype == -1) travmtype = 2;
                        }
                        else
                        {
                            if (ntr[1] > 0)
                            {
                                nt[1]++;
                                if (travmtype == -1) travmtype = 1;
                            }
                            else
                            {
                                nt[0]++;
                                if (travmtype == -1) travmtype = 0;
                            }
                        }
                    }

                    var nametr = new[] { "легкую", "среднюю", "тяжелую", "боевую" };
                    if (travmtype != -1)
                    {
                        if (sbtt[travmtype].Length > 0)
                        {
                            sbtt[travmtype].Append(':');
                        }

                        sbtt[travmtype].Append(userName);
                    }

                    sb.Length = 0;
                    sb.Append(userName);
                    sb.Append(" [");
                    sb.Append(userLevel);
                    sb.Append(']');
                    var trmi1 = new ToolStripMenuItem(sb.ToString()) { Image = Properties.Resources._16x16_private, Tag = userName };
                    if (AppVars.MainForm != null) trmi1.Click += AppVars.MainForm.OnTravmFastToolStripMenuItemClick;
                    trmi.DropDownItems.Add(trmi1);
                    var trmi3 = new ToolStripMenuItem("Открыть инфу") { Image = Properties.Resources._16x16_info, Tag = userName };
                    if (AppVars.MainForm != null) trmi3.Click += AppVars.MainForm.OnTravmInfoToolStripMenuItemClick;
                    trmi.DropDownItems.Add(trmi3);
                    trmi.DropDownItems.Add(new ToolStripSeparator());

                    var icontr = new[]
                                     {
                                         Properties.Resources._15x12_tr1,
                                         Properties.Resources._15x12_tr2,
                                         Properties.Resources._15x12_tr3,
                                         Properties.Resources._15x12_tr4
                                     };
                    for (var t = 0; t < 4; t++)
                    {
                        if (ntr[t] <= 0) continue;
                        var tst = new ToolStripMenuItem("Лечить " + nametr[t] + " травму")
                        {
                            Image = icontr[t],
                            ImageScaling = ToolStripItemImageScaling.None,
                            Tag = userName + ":" + (t + 1)
                        };
                        if (AppVars.MainForm != null) tst.Click += FormMain.OnTravmCureToolStripMenuItemClick;
                        trmi.DropDownItems.Add(tst);
                    }

                    trmi.DropDownItems.Add(new ToolStripSeparator());

                    var trmi2 = new ToolStripMenuItem("Отправить рекламу") { Tag = userName };
                    if (AppVars.MainForm != null) trmi2.Click += FormMain.OnTravmAdvToolStripMenuItemClick;
                    trmi.DropDownItems.Add(trmi2);
                }

                trlist.Add(trmi);
            }

            if (trlist.Count > 0)
            {
                sbt.Append(trlist.Count);
                sbt.Append(": ");
                for (var j = 0; j < 4; j++)
                {
                    if (nt[j] == 0)
                    {
                        sbt.Append('-');
                    }
                    else
                    {
                        sbt.Append(nt[j]);
                    }

                    if (j < 3)
                    {
                        sbt.Append('/');
                    }
                }

                var trek = new ToolStripMenuItem("Реклама")
                {
                    Image = Properties.Resources._16x16_private,
                };

                var namett = new[] { "легкие", "средние", "тяжелые", "боевые" };
                for (var i = 0; i < 4; i++)
                {
                    if (sbtt[i].Length <= 0) continue;
                    var tt = new ToolStripMenuItem("Реклама тем, у кого " + namett[i]) { Image = Properties.Resources._16x16_private, Tag = (i + 1) + ":" + sbtt[i] };
                    if (AppVars.MainForm != null) tt.Click += FormMain.OnTravmAskToolStripMenuItemClick;
                    trek.DropDownItems.Add(tt);
                }

                var trall = new ToolStripMenuItem("Реклама всем травмированным")
                {
                    Image = Properties.Resources._16x16_private,
                    Tag = sbtx.ToString()
                };

                if (AppVars.MainForm != null) trall.Click += FormMain.OnTravmAdvAllToolStripMenuItemClick;
                trek.DropDownItems.Add(trall);

                var trallchat = new ToolStripMenuItem("Реклама в общий чат");
                if (AppVars.MainForm != null) trallchat.Click += FormMain.OnTravmChatToolStripMenuItemClick;
                trek.DropDownItems.Add(trallchat);

                trlist.Add(trek);
            }

            try
            {
                if (AppVars.MainForm != null)
                    AppVars.MainForm.BeginInvoke(
                        new UpdateRoomDelegate(AppVars.MainForm.UpdateRoom),
                        new object[] { pvlist.ToArray(), sbt.ToString(), trlist.ToArray() });
            }
            catch (InvalidOperationException)
            {
            }

            resultFilterProcRoom.NumCharsInRoom = par.Length;
            if (enemyAttack.Count > 0)
            {
                var filtredenemyAttack = new List<string>();
                foreach (var nick in filtredenemyAttack)
                {
                    if (IsCharInBlackList(nick))
                        continue;

                    filtredenemyAttack.Add(nick);
                }

                resultFilterProcRoom.EnemyAttack = filtredenemyAttack.Count > 0 ? enemyAttack[Dice.Make(filtredenemyAttack.Count)] : enemyAttack[Dice.Make(enemyAttack.Count)];
            }

            return resultFilterProcRoom;
        }

        private static void FilterGetWalkers(string html)
        {
            if (!AppVars.DoShowWalkers)
            {
                return;
            }

            var mLocnow = HelperStrings.SubString(html, "<font class=placename><b>", "</b>");
            if (string.IsNullOrEmpty(mLocnow))
            {
                return;
            }

            var arg = HelperStrings.SubString(html, "new Array(", ");");
            if (string.IsNullOrEmpty(arg))
            {
                return;
            }

            var par = arg.Split(new[] { @"""," }, StringSplitOptions.RemoveEmptyEntries);
            if (par.Length == 0)
            {
                return;
            }

            var mDnow = new Dictionary<string, string>();
            for (var i = 0; i < par.Length; i++)
            {
                var pararg = par[i].Substring(3, par[i].Length - 3);
                var pars = pararg.Split(':');
                if (pars.Length < 3)
                {
                    continue;
                }

                if (pars[1].IndexOf("<i>", StringComparison.OrdinalIgnoreCase) == -1)
                    mDnow.Add(pars[1], pararg);
            }

            if ((AppVars.Profile.MapLocation == AppVars.MyCoordOld) && (mLocnow == AppVars.MyLocOld))
            {
                var myDleft = new Dictionary<string, string>();
                var keyleft = AppVars.MyCharsOld.Keys;
                foreach (var kl in keyleft)
                {
                    if (kl != null && !mDnow.ContainsKey(kl))
                    {
                        if (kl.Equals(AppVars.Profile.UserNick, StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (AppVars.MainForm != null)
                                AppVars.MainForm.WriteChatMsgSafe("<b><font color=#01A9DB>Мы ушли в невид</font></b>");
                        }
                        else
                            myDleft.Add(kl, AppVars.MyCharsOld[kl]);
                    }
                }

                var myDcome = new Dictionary<string, string>();
                var keycome = mDnow.Keys;
                foreach (var kc in keycome)
                {
                    if (kc != null && !AppVars.MyCharsOld.ContainsKey(kc))
                    {
                        if (kc.Equals(AppVars.Profile.UserNick, StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (AppVars.MainForm != null)
                                AppVars.MainForm.WriteChatMsgSafe("<b><font color=#DF0101>Мы вышли из невида!</font></b>");
                        }
                        else
                            myDcome.Add(kc, mDnow[kc]);
                    }
                }

                var diffn = AppVars.MyNevids - AppVars.MyNevidsOld;
                if ((myDleft.Count != 0) || (myDcome.Count != 0) || (diffn != 0))
                {
                    var sb = new StringBuilder();
                    var i = 0;
                    if (diffn > 0)
                    {
                        i = 1;
                        sb.Append("<font color=#5D7C91><b>");
                        if (diffn == 1)
                        {
                            sb.Append("Невидимка");
                        }
                        else
                        {
                            sb.Append(diffn);
                            sb.Append(" невидимок");
                        }

                        sb.Append("</b></font>");
                    }

                    if (myDcome.Count > 0)
                    {
                        keycome = myDcome.Keys;

                        foreach (var kc in keycome)
                        {
                            if (i > 0)
                            {
                                sb.Append(", ");
                            }

                            i++;
                            sb.Append(HtmlChar(myDcome[kc]));
                        }
                    }

                    if (i > 0)
                    {
                        sb.Append(i > 1 ? " приходят в локацию" : " приходит в локацию");
                    }

                    AppVars.MyWalkers1 = sb.ToString();
                    sb.Length = 0;
                    i = 0;
                    if (diffn < 0)
                    {
                        i = 1;
                        sb.Append("<font color=#5D7C91><b>");
                        if (diffn == -1)
                        {
                            sb.Append("Невидимка");
                        }
                        else
                        {
                            sb.Append(-diffn);
                            sb.Append(" невидимок");
                        }

                        sb.Append("</b></font>");
                    }

                    if (myDleft.Count > 0)
                    {
                        keyleft = myDleft.Keys;

                        foreach (var kl in keyleft)
                        {
                            if (i > 0)
                            {
                                sb.Append(", ");
                            }

                            i++;
                            sb.Append(HtmlChar(myDleft[kl]));
                        }
                    }

                    if (i > 0)
                    {
                        sb.Append(i > 1 ? " покидают локацию" : " покидает локацию");
                    }

                    AppVars.MyWalkers2 = sb.ToString();
                }
            }

            AppVars.MyCoordOld = AppVars.Profile.MapLocation;
            AppVars.MyLocOld = mLocnow;
            AppVars.MyCharsOld.Clear();
            AppVars.MyCharsOld = new Dictionary<string, string>(mDnow);
            AppVars.MyNevidsOld = AppVars.MyNevids;

            if (!string.IsNullOrEmpty(AppVars.MyWalkers1))
            {
                EventSounds.PlayAlarm();
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateChatDelegate(AppVars.MainForm.UpdateChat), AppVars.MyWalkers1);
                    }
                }
                catch (InvalidOperationException)
                {
                }

                AppVars.MyWalkers1 = string.Empty;
            }

            if (!string.IsNullOrEmpty(AppVars.MyWalkers2))
            {
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                            new object[] { AppVars.MyWalkers2 });
                    }
                }
                catch (InvalidOperationException)
                {
                }

                AppVars.MyWalkers2 = string.Empty;
            }
        }

        private static string HtmlChar(string schar)
        {
            var strArray = schar.Split(new[] { ':' });
            var nnSec = strArray[1];
            var login = strArray[1];
            while (nnSec.Contains("+"))
            {
                nnSec = nnSec.Replace("+", "%2B");
            }

            if (login.Contains("<i>"))
            {
                login = login.Replace("<i>", String.Empty);
                login = login.Replace("</i>", String.Empty);
                nnSec = nnSec.Replace("<i>", String.Empty);
                nnSec = nnSec.Replace("</i>", String.Empty);
            }

            var ss = string.Empty;
            var altadd = string.Empty;
            if (strArray[3].Length > 1)
            {
                var signArray = strArray[3].Split(new[] { ';' });
                if (signArray[2].Length > 1)
                {
                    altadd = " (" + signArray[2] + ")";
                }

                ss =
                    "<img src=http://image.neverlands.ru/signs/" +
                    signArray[0] +
                    @" width=15 height=12 align=absmiddle alt=""" +
                    signArray[1] +
                    altadd +
                    @""">&nbsp;";
            }

            var sleeps = string.Empty;
            if (strArray[4].Length > 1)
            {
                sleeps =
                    @"<img src=http://image.neverlands.ru/signs/molch.gif width=15 height=12 border=0 alt=""" +
                    strArray[4] +
                    @""" align=absmiddle>";
            }

            var ign = string.Empty;
            if (strArray[5] == "1")
            {
                ign =
                    @"<a href=""javascript:ch_clear_ignor('" +
                    login +
                    @"');""><img src=http://image.neverlands.ru/signs/ignor/3.gif width=15 height=12 border=0 alt=""Снять игнорирование""></a>";
            }

            var inj = string.Empty;
            if (strArray[6] != "0")
            {
                inj = @"<img src=http://image.neverlands.ru/chat/tr4.gif border=0 width=15 height=12 alt=""" +
                      strArray[6] +
                      @""" align=absmiddle>";

                if (strArray[6].Contains("боевая"))
                {
                    strArray[1] = @"<font color=""#666600"">" + strArray[1] + "</font>";
                }
                else
                {
                    if (strArray[6].Contains("тяжелая"))
                    {
                        strArray[1] = @"<font color=""#c10000"">" + strArray[1] + "</font>";
                    }
                    else
                    {
                        if (strArray[6].Contains("средняя"))
                        {
                            strArray[1] = @"<font color=""#e94c69"">" + strArray[1] + "</font>";
                        }
                        else
                        {
                            if (strArray[6].Contains("легкая"))
                            {
                                strArray[1] = @"<font color=""#ef7f94"">" + strArray[1] + "</font>";
                            }
                        }
                    }
                }
            }

            var psg = string.Empty;
            if (strArray[7] != "0")
            {
                var dilers = new[] { "", "Дилер", "", "", "", "", "", "", "", "", "", "Помощник дилера" };
                psg =
                    "<img src=http://image.neverlands.ru/signs/d_sm_" +
                    strArray[7] +
                    @".gif width=15 height=12 align=absmiddle border=0 alt=""" +
                    dilers[int.Parse(strArray[7])] +
                    @""">&nbsp;";
            }

            var align = string.Empty;
            if (strArray[8] != "0")
            {
                var signArray = strArray[8].Split(new[] { ';' });
                if (signArray.Length >= 2)
                {
                    align =
                        "<img src=http://image.neverlands.ru/signs/" +
                        signArray[0] +
                        @" width=15 height=12 align=absmiddle border=0 alt=""" +
                        signArray[1] +
                        @""">&nbsp";
                }
            }

            return
                @"<a href=""#"" onclick=""top.say_private('" +
                login +
                @"');""><img src=http://image.neverlands.ru/chat/private.gif width=11 height=12 border=0 align=absmiddle></a>&nbsp;" +
                psg +
                align +
                ss +
                @"<a class=""activenick"" href=""#"" onclick=""top.say_to('" +
                login +
                @"');""><font class=nickname><b>" +
                strArray[1] +
                "</b></a>[" +
                strArray[2] +
                @"]</font><a href=""http://www.neverlands.ru/pinfo.cgi?" +
                nnSec +
                @""" onclick=""window.open(this.href);""><img src=http://image.neverlands.ru/chat/info.gif width=11 height=12 border=0 align=absmiddle></a>" +
                sleeps +
                ign +
                inj;
        }
    }
}
