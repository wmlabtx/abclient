using System.Text.RegularExpressions;
using ABClient.Helpers;

namespace ABClient.PostFilter
{
    using System;
    using System.Text;
    using MyHelpers;
    using Properties;

    internal static partial class Filter
    {
        /*
        private static byte[] AddJson(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            html = Resources.json2 + " " + html;
            return Russian.Codepage.GetBytes(html);
        }
        */

        internal static byte[] PreProcess(string address, byte[] array)
        {
            if (string.IsNullOrEmpty(address))
                return null;

            /*
            if (address.Equals("http://forum.neverlands.ru/action/"))
            {
                var html = Russian.Codepage.GetString(array);
                var textOrg = HelperStrings.SubString(html, "&reply_mess=", "&s=");
                if (!string.IsNullOrEmpty(textOrg))
                {
                    //var textOrgDecoded = HelperConverters.NickDecode(textOrg);
                    string[] rug = 
                        { "Ёбаный в рыло! Ты конченый абшник", "Ты засраный хуедав моржовый", "пиздюк блошиный на инфу смотри )))", "Ты пиздоебательный недоебок", "Жопосер перепиздюханный ботами",
                          "Ебать тебя немытым свитком!", "оркский залупенец! ко мне гони!", "Пиздострадатель???", "Ебаны с хуем во лбу", "Пиздоебливая абешка", "Ваш глава - пиздотёрый мудозвон )))",
                          "Пиздоноздря, ты лижешь у админов? )", "пиздячина хуерыльная", "ЧС тебе, хуидло!", "Боеаую тебе в пиздилищу", "Хуилище", "Обезьянья шишка", "Ебливая залупа!", "Ты ставленый раком пиздосос",
                          "куроёб ты мальчик", "пиздовый хуебун", "пиздаш долбанный ограми", "давно боевые не ловил пиздопроситель сучий", "Пиздалон хуебарный держи подарок", "Пиздоворот захуяченный", "хуярез мандавошный",
                          "пиздасер безжопый, иди на аб", "жополиз пиздадавленный", "долбоебатина федя", "держи подарок, пиздулия заштопанная", "ты клоповыёбистый ублюдоёб и клан твой такой же", "Пиздоглист",
                          "пиздолиз шпокнутый", "пиздень напиханная ботами", "Ялдак )", "пиздокопатель с рынка )", "восмьиконечная хуюла Форпоста", "вертохуй из клана вертохуев", "пиздоглазая нубятина",
                          "хуеворот пиздотыренный", "пиздосербало хуетертое", "хуебур пиздуянистый", "пиздоверзилище кукуйское", "пиздюшки тебе", "двуголовое хуило ты в ЧС!",
                          "Пиздодырявина )))", "пиздожал, ты мою инфу видел?", "хуеблядский нубяра", "пиздоногая блядевина еби тебя конем до селезенок", "грушу тебе в пизду и боевую туда же",
                          "хуерык одноногий блаж пей )))", "глиста пиздодырная из клана Глист )", "квесты делай гусеёб )", "пизда горбатая соси у топов", "ебал гужи!!!", "давно пизды не видал?", "хуй твой блошиный",
                          "хуевина", "на пизду тебе боевая???", "достига за пиздоеблю тебе", "мудями позванивай на осаду беги", "дави достиги хуем" };

                    var message = rug[Dice.Make(rug.Length)];
                    var textEncoded = HelperConverters.NickEncode(message);

                    var cd = AppVars.ServerDateTime;
                    for (var i = 0; i < 381; i++)
                    {
                        var ed = cd.AddDays(i);
                        var str = $"((++{AppVars.Profile.UserNick.ToUpperInvariant()}***{ed.ToString("yyyyMMdd")}++))";
                        var buffer = Encoding.UTF8.GetBytes(str);
                        var md5 = MD5.Create();
                        var hashbuffer = md5.ComputeHash(buffer);
                        var m = "ОЕАИНТСРВЛКМПУЯГ";
                        var sb = new StringBuilder();
                        for (var j = 0; j < 16; j++)
                        {
                            sb.Append(m[hashbuffer[j] >> 4]);
                            sb.Append(m[hashbuffer[j] & 0xF]);
                            if (((j + 1) % 4) == 0 && (j != 15))
                                sb.Append('-');
                        }

                        if (!sb.ToString().Equals(AppVars.Profile.UserKey.Trim(), StringComparison.CurrentCultureIgnoreCase))
                            continue;

                        cd = DateTime.MinValue;
                        break;
                    }

                    if (cd > DateTime.MinValue)
                    {
                        html = html.Replace(textOrg, textEncoded);
                        return Russian.Codepage.GetBytes(html);
                    }
                }
            }

            //var htmlx = Russian.Codepage.GetString(array);
            //File.WriteAllText("os1$.txt", htmlx);
            */

            return array;
        }

        internal static byte[] Process(string address, byte[] array)
        {
            if (string.IsNullOrEmpty(address))
                return null;

            var html = Russian.Codepage.GetString(array);

            if (address.Contains(".js"))
            {
                if (address.Contains("/js/hp.js"))
                    return HpJs(array);

                // 2/11/2017 - <SCRIPT src="/js/map.js?v=4"></SCRIPT>
                if (address.Contains("/js/map.js"))
                    return MapJs(array);

                if (address.Contains("/arena"))
                    return ArenaJs();

                /*
                if (address.Contains("/tarena") ||
                    address.Contains("/castle") ||
                    address.Contains("/tower") ||
                    address.Contains("/outpost") ||
                    address.Contains("/cityhall") ||
                    address.Contains("/clanhall"))
                    return AddJson(array);
                    */

                if (address.EndsWith("/js/game.js", StringComparison.OrdinalIgnoreCase))
                {
                    return GameJs(array);
                }

                if (address.IndexOf("pinfo_v01.js", StringComparison.Ordinal) != -1)
                {
                    return PinfoJs(array);
                }

                if (address.Contains("/js/fight_v"))
                {
                    return FightJs(array);
                }

                if (address.Contains("/js/building"))
                {
                    return BuildingJs(array);
                }

                if (address.EndsWith("/js/hpmp.js", StringComparison.OrdinalIgnoreCase))
                {
                    return HpmpJs();
                }

                if (address.EndsWith("/ch/ch_msg_v01.js", StringComparison.OrdinalIgnoreCase))
                {
                    return ChMsgJs(array);
                }

                if (address.EndsWith("/js/pv.js", StringComparison.OrdinalIgnoreCase))
                {
                    return PvJs(array);
                }

                if (address.EndsWith("/ch/ch_list.js", StringComparison.OrdinalIgnoreCase))
                {
                    return ChListJs();
                }

                if (address.EndsWith("/js/svitok.js", StringComparison.OrdinalIgnoreCase))
                {
                    return SvitokJs(array);
                }

                if (address.EndsWith("/js/slots.js", StringComparison.OrdinalIgnoreCase))
                {
                    return SlotsJs(array);
                }

                if (address.IndexOf("/js/logs", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    return LogsJs(array);
                }

                if (address.IndexOf("/js/shop", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    return ShopJs(array);
                }

                if (address.IndexOf("/js/forum/forum_topic.js", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return ForumTopicJs(array);
                }
            }

            var pos1 = address.IndexOf(".js", StringComparison.CurrentCultureIgnoreCase);
            if (pos1 < 0)
            {
                var pos2 = address.IndexOf(".swf", StringComparison.CurrentCultureIgnoreCase);
                if (pos2 < 0)
                {
                    Log.Write(address, html);
                }
            }

            if (address.StartsWith("http://www.neverlands.ru/index.cgi", StringComparison.OrdinalIgnoreCase) ||
                address.Equals("http://www.neverlands.ru/", StringComparison.OrdinalIgnoreCase))
            {
                return IndexCgi(array);
            }

            if (address.StartsWith("http://www.neverlands.ru/pinfo.cgi", StringComparison.OrdinalIgnoreCase))
            {
                return RemoveDoctype(array);
            }

            if (address.StartsWith("http://www.neverlands.ru/pbots.cgi", StringComparison.OrdinalIgnoreCase))
            {
                return RemoveDoctype(array);
            }

            if (address.StartsWith("http://forum.neverlands.ru/", StringComparison.OrdinalIgnoreCase))
            {
                return RemoveDoctype(array);
            }

            if (address.StartsWith("http://www.neverlands.ru/game.php", StringComparison.OrdinalIgnoreCase))
            {
                return GamePhp(array);
            }

            if (address.StartsWith("http://www.neverlands.ru/main.php", StringComparison.OrdinalIgnoreCase))
            {
                AppVars.NextCheckNoConnection = DateTime.Now.AddMinutes(5);
                return MainPhp(address, array);
            }

            if (address.StartsWith("http://www.neverlands.ru/ch/msg.php", StringComparison.OrdinalIgnoreCase))
            {
                return MsgPhp(array);
            }

            if (address.StartsWith("http://www.neverlands.ru/ch/but.php", StringComparison.OrdinalIgnoreCase))
            {
                return ButPhp(array);
            }

            if (address.StartsWith("http://www.neverlands.ru/gameplay/trade.php", StringComparison.OrdinalIgnoreCase))
            {
                return AppVars.Profile.TorgActive ? TradePhp(array) : array;
            }

            if (address.StartsWith("http://www.neverlands.ru/gameplay/ajax/map_act_ajax.php", StringComparison.OrdinalIgnoreCase))
            {
                return MapActAjaxPhp(array);
            }

            if (address.StartsWith("http://www.neverlands.ru/gameplay/ajax/fish_ajax.php", StringComparison.OrdinalIgnoreCase))
            {
                return FishAjaxPhp(array);
            }

            if (address.StartsWith("http://www.neverlands.ru/gameplay/ajax/shop_ajax.php", StringComparison.OrdinalIgnoreCase))
            {
                return ShopAjaxPhp(array);
            }

            if (address.StartsWith("http://www.neverlands.ru/gameplay/ajax/roulette_ajax.php", StringComparison.OrdinalIgnoreCase))
            {
                return RouletteAjaxPhp(array);
            }
            
            if (address.StartsWith("http://www.neverlands.ru/ch.php?lo=", StringComparison.OrdinalIgnoreCase))
            {
                return ChRoomPhp(array);
            }

            if (address.IndexOf("/ch.php?0", StringComparison.OrdinalIgnoreCase) != -1)
            {
                return ChZero(array);
            }

            if (address.StartsWith(Resources.AddressPInfo))
            {
                return Pinfo(array);
            }

            return array;
        }

        private static string BuildRedirect(string description, string link)
        {
            var sb = new StringBuilder(HelperErrors.Head());
            sb.Append(description);
            sb.Append(
                @"<script language=""JavaScript"">" +
                @"  window.location = """);
            sb.Append(link);
            sb.Append(
                @""";</script></body></html>");
            return sb.ToString();
        }

        private static readonly Regex DocType = new Regex(@"<!DOCTYPE[^>[]*(\[[^]]*\])?>");

        private static string RemoveDoctype(string html)
        {
            return DocType.Replace(html, string.Empty);
        }

        private static byte[] RemoveDoctype(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            html = RemoveDoctype(html);
            return Russian.Codepage.GetBytes(html);
        }
    }
}