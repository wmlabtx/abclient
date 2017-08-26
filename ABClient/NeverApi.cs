using System;
using System.Collections.Generic;
using System.Threading;
using ABClient.MyHelpers;

namespace ABClient
{
    public static class NeverApi
    {
        private static readonly Dictionary<string, string> NameToId = new Dictionary<string, string>();
        private static readonly ReaderWriterLock NameToIdLock = new ReaderWriterLock();

        private static string GetUserId(string nick)
        {
            string id;
            if (NameToId.TryGetValue(nick, out id))
                return id;

            var encnick = HelperConverters.NickEncode(nick);
            var url = $"http://www.neverlands.ru/modules/api/getid.cgi?{encnick}";
            var data = GetInfo(url);
            if (string.IsNullOrEmpty(data))
                return null;

            var spar = data.Split('|');
            if (spar.Length != 2)
                return null;

            id = spar[0];
            var name = spar[1];

            try
            {
                NameToIdLock.AcquireWriterLock(1000);
                try
                {
                    if (NameToId.ContainsKey(name))
                        NameToId[name] = id;
                    else
                        NameToId.Add(name, id);
                }
                finally
                {
                    NameToIdLock.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }

            return id;
        }

        public static UserInfo GetAll(string nick)
        {
            var userInfo = new UserInfo();

            var id = GetUserId(nick);
            if (string.IsNullOrEmpty(id))
                return null;

            var data = GetInfo($"http://www.neverlands.ru/modules/api/info.cgi?playerid={id}&info=1&hmu=1&effects=1&slots=1");
            /*
                1|tnsx4hoq.gif:Шлем Гладиатора:|0|0|45|0|60|0|150@abz4cs8n.gif:Амулет Ацтеков:|3|5|5|0|80|0|110@vph7940g.gif:Секира Солнца:|36|50|0|62|25|0|180@u09ops6g.gif:Пояс Смелости:|0|0|15|0|60|0|108@sl_l_4.gif:Слот для содержимого пояса@sl_l_4.gif:Слот для содержимого пояса@sl_l_4.gif:Слот для содержимого пояса@d6ushtk9.gif:Кованые Сапоги:|10|12|24|0|90|0|100@sl_r_0.gif:Слот для кармана@sl_r_1.gif:Слот для содержимого кармана@kf704b5i.gif:Клёпаные Наручи:|8|11|20|20|60|0|100@yvmqa2cg.gif:Кольчужные Перчатки:|4|9|14|10|50|0|90@sl_l_2.gif:Слот для оружия/щита@81rblgew.gif:Кольцо Легендарной Удачи:|0|0|0|0|0|0|300@m5hjvieu.gif:Кольцо Легендарной Силы:|0|0|0|0|0|0|300@u2x1l94a.gif:Броня Единорога:|0|0|64|0|90|0|250@
                2|
                3|Черный|16|0|n|none|||0|1|0|0|0|0||18834655
                4|0|785|0|112|77
            */
            if (string.IsNullOrEmpty(data))
                return null;

            var sp = data.Split('\n');
            if (sp.Length != 5)
                return null;

            userInfo.SlotsCodes = new string[0];
            userInfo.SlotsNames = new string[0];

            var sp1 = sp[0].Substring(2).Split('@');
            if (sp1.Length < 16)
                return null;

            userInfo.SlotsCodes = new string[16];
            userInfo.SlotsNames = new string[16];

            for (var i = 0; i < 16; i++)
            {
                var sps = sp1[i].Split(':');
                if (sps.Length < 2)
                    return null;

                userInfo.SlotsCodes[i] = sps[0];
                userInfo.SlotsNames[i] = sps[1];
            }

            userInfo.EffectsCodes = new string[0];
            userInfo.EffectsNames = new string[0];
            userInfo.EffectsSizes = new string[0];
            userInfo.EffectsLefts = new string[0];

            if (sp[1].Length > 2)
            {
                var sp2 = sp[1].Substring(2).Split('@');
                userInfo.EffectsCodes = new string[sp2.Length];
                userInfo.EffectsNames = new string[sp2.Length];
                userInfo.EffectsSizes = new string[sp2.Length];
                userInfo.EffectsLefts = new string[sp2.Length];
                for (var i = 0; i < sp2.Length; i++)
                {
                    var sps = sp2[i].Split('.');
                    if (sps.Length < 4)
                        return null;

                    userInfo.EffectsCodes[i] = sps[0];
                    userInfo.EffectsNames[i] = sps[1];
                    userInfo.EffectsSizes[i] = sps[2];
                    userInfo.EffectsLefts[i] = sps[3];
                }
            }

            var sp3 = sp[2].Substring(2).Split('|');
            if (sp3.Length < 14)
                return null;

            userInfo.Nick = sp3[0].Trim();
            userInfo.Level = sp3[1];
            userInfo.Align = sp3[2];
            userInfo.ClanCode = sp3[3];
            userInfo.ClanSign = sp3[4];
            userInfo.ClanName = sp3[5];
            userInfo.ClanStatus = sp3[6];
            userInfo.Sex = sp3[7];
            if (!sp3[8].Equals("0"))
                userInfo.Disabled = true;

            if (!sp3[9].Equals("0"))
                userInfo.Jailed = true;

            userInfo.ChatMuted = sp3[10];
            userInfo.ForumMuted = sp3[11];
            if (!sp3[12].Equals("0"))
                userInfo.Online = true;

            userInfo.Location = sp3[13];
            userInfo.FightLog = sp3[14];

            var sp4 = sp[3].Substring(2).Split('|');
            if (sp4.Length < 5)
                return null;

            int.TryParse(sp4[0], out userInfo.HpCur);
            int.TryParse(sp4[1], out userInfo.HpMax);
            int.TryParse(sp4[2], out userInfo.MaCur);
            int.TryParse(sp4[3], out userInfo.MaMax);
            int.TryParse(sp4[4], out userInfo.Tied);
            userInfo.Tied = 100 - userInfo.Tied;

            return userInfo;
        }

        public static string GetPInfo(string nick)
        {
            var url = HelperConverters.AddressEncode(string.Concat("http://neverlands.ru/pinfo.cgi?", nick));
            return GetInfo(url);
        }

        public static string GetFlog(string flog)
        {
            var url = HelperConverters.AddressEncode(string.Concat("http://neverlands.ru/logs.fcg?fid=", flog));
            return GetInfo(url);
        }

        private static string GetInfo(string url)
        {
            string html = null;
            using (var wc = new CookieAwareWebClient { Proxy = AppVars.LocalProxy })
            {
                try
                {
                    IdleManager.AddActivity();
                    var buffer = wc.DownloadData(url);
                    if (buffer != null)
                    {
                        html = AppVars.Codepage.GetString(buffer);
                        if (html.IndexOf("Cookie...", StringComparison.CurrentCultureIgnoreCase) != -1)
                        {
                            buffer = wc.DownloadData(url);
                            if (buffer != null)
                            {
                                html = AppVars.Codepage.GetString(buffer);
                            }
                        }
                    }
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {
                }
                finally
                {
                    IdleManager.RemoveActivity();
                }
            }

            return html;
        }
    }
}
