using System.Collections.Generic;
using ABClient.ExtMap;

namespace ABClient.PostFilter
{
    using System;

    internal static partial class Filter
    {
        private static string MainPhpCityNavigation(string html)
        {
            if (html.IndexOf("<AREA SHAPE=", StringComparison.OrdinalIgnoreCase) == -1 &&
                html.IndexOf(@"url(http://image.neverlands.ru/cities/forpost/", StringComparison.OrdinalIgnoreCase) == -1)
            {
                return string.Empty;
            }

            var area = new string[0];
            switch (AppVars.AutoMovingCityGate)
            {
                case CityGateType.ForpostRightToLeftGate:
                    area = new[]
                    {
                        "Выход из города",
                        "Перейти на городскую площадь",
                        "Перейти в жилой квартал"
                    };
                    break;

                case CityGateType.ForpostLeftToRightGate:
                    area = new[]
                    {
                        "Выход из города",
                        "Перейти в квартал закона",
                        "Перейти в жилой квартал"
                    };
                    break;

                case CityGateType.OktalLeftToRightGate:
                    area = new[]
                    {
                        "Выход из города, Восточные Ворота",
                        "Перейти на Площадь Гильдий",
                        "Перейти в Деловой Квартал",
                        "Перейти в Промышленный Квартал",
                        "Перейти в Торговый Квартал"
                    };
                    break;

                case CityGateType.OktalRightToLeftGate:
                    area = new[]
                    {
                        "Выход из города, Западные Ворота",
                        "Перейти на Центральную Площадь",
                        "Перейти в Торговый Квартал",
                        "Перейти в Промышленный Квартал",
                        "Перейти в Деловой Квартал"
                    };
                    break;

                case CityGateType.OktalLeftToBottomGate:
                    area = new[]
                    {
                        "Выход из города, Южные Ворота",
                        "Перейти к Конюшням",
                        "Перейти в Промышленный Квартал",
                        "Перейти в Торговый Квартал"
                    };
                    break;

                case CityGateType.OktalRightToBottomGate:
                    area = new[]
                    {
                        "Выход из города, Южные Ворота",
                        "Перейти к Конюшням"
                    };
                    break;

                case CityGateType.OktalBottomToLeftGate:
                    area = new[]
                    {
                        "Выход из города, Западные Ворота",
                        "Перейти на Центральную Площадь",
                        "Перейти в Торговый Квартал",
                        "Перейти в Промышленный Квартал"
                    };
                    break;

                case CityGateType.OktalBottomToRightGate:
                    area = new[]
                    {
                        "Выход из города, Восточные Ворота",
                        "Перейти на Площадь Гильдий"
                    };
                    break;
            }

            if (area.Length > 0)
            {
                if (AppVars.AutoMovingCityGate == CityGateType.ForpostLeftToRightGate)
                {
                    if (
                        (html.IndexOf("Выход из города", StringComparison.OrdinalIgnoreCase) != -1) &&
                        (html.IndexOf("Перейти в деловой квартал", StringComparison.OrdinalIgnoreCase) != -1)
                        )
                    {
                        var newhtml = MainPhpCityArea(html, "Перейти в жилой квартал");
                        if (!string.IsNullOrEmpty(newhtml))
                        {
                            return newhtml;
                        }                        
                    }

                    if (
                        (html.IndexOf("Выход из города", StringComparison.OrdinalIgnoreCase) != -1) &&
                        (html.IndexOf("Перейти в деловой квартал", StringComparison.OrdinalIgnoreCase) == -1)
                        )
                    {
                        var newhtml = MainPhpCityArea(html, "Выход из города");
                        if (!string.IsNullOrEmpty(newhtml))
                        {
                            return newhtml;
                        }
                    }
                }

                if (AppVars.AutoMovingCityGate == CityGateType.ForpostRightToLeftGate)
                {
                    if (
                        (html.IndexOf("Выход из города", StringComparison.OrdinalIgnoreCase) != -1) &&
                        (html.IndexOf("Перейти в деловой квартал", StringComparison.OrdinalIgnoreCase) == -1)
                        )
                     {
                        var newhtml = MainPhpCityArea(html, "Перейти в жилой квартал");
                        if (!string.IsNullOrEmpty(newhtml))
                        {
                            return newhtml;
                        }
                    }

                    if (
                        (html.IndexOf("Выход из города", StringComparison.OrdinalIgnoreCase) != -1) &&
                        (html.IndexOf("Перейти в деловой квартал", StringComparison.OrdinalIgnoreCase) != -1)
                        )
                    {
                        var newhtml = MainPhpCityArea(html, "Выход из города");
                        if (!string.IsNullOrEmpty(newhtml))
                        {
                            return newhtml;
                        }
                    }
                }

                for (var i = 0; i < area.Length; i++)
                {
                    var newhtml = MainPhpCityArea(html, area[i]);
                    if (!string.IsNullOrEmpty(newhtml))
                    {
                        return newhtml;
                    }
                }
            }

            return string.Empty;
        }

        private static string MainPhpCityArea(string html, string area)
        {
            // HREF="main.php?get_id=56&act=10&go=forpost3&vcode=5de3e0e983b05e33abf99d53e4ace2b6" 
            // COORDS="126,248,156,249,153,242,169,241,177,234,165,209,144,227,142,221,129,235" 
            // onmouseover="tooltip(this,'Перейти в деловой квартал')"

            // <div style="position:absolute; left: 900; top: 505;">
            // <a href="main.php?get_id=56&act=10&go=forpost1&vcode=33764ca99b7a854057a4200e28cc7e2c">
            // <img src="http://image.neverlands.ru/cities/forpost/loc5_arrow_2.png" onmouseover="this.src = 'http://image.neverlands.ru/cities/forpost/loc5_arrow_2_hl.png'; 
            // tooltip(this,'Перейти в деловой квартал');" onmouseout="this.src='http://image.neverlands.ru/cities/forpost/loc5_arrow_2.png'; hide_info(this);" /></a>
            //
            //

            /*
              <div style="width: 1250px; height: 600px; margin: 0 auto; position: relative; background: url(http://image.neverlands.ru/cities/forpost/loc5_bg_n.jpg)">
              <div style="position:absolute; left: 154; top: 167;"><a href="main.php?get_id=56&act=10&go=build&pl=bar0&vcode=420ebb3d63e219797d03db2e9242b27a"><img src="http://image.neverlands.ru/cities/forpost/loc5_a_n.png" onmouseover="this.src = 'http://image.neverlands.ru/cities/forpost/loc5_a_n_hl.png'; tooltip(this,'Òàâåðíà');" onmouseout="this.src='http://image.neverlands.ru/cities/forpost/loc5_a_n.png'; hide_info(this);" /></a></div>
              <div style="position:absolute; left: 374; top: 0;"><a href="main.php?get_id=56&act=10&go=arena&vcode=98c621c0ec59516304a1de99aa0f743f"><img src="http://image.neverlands.ru/cities/forpost/loc5_b_n.png" onmouseover="this.src = 'http://image.neverlands.ru/cities/forpost/loc5_b_n_hl.png'; tooltip(this,'Àðåíà äëÿ ïîåäèíêîâ');" onmouseout="this.src='http://image.neverlands.ru/cities/forpost/loc5_b_n.png'; hide_info(this);" /></a></div>
              <div style="position:absolute; left: 96; top: 303;"><a href="main.php?get_id=56&act=10&go=build&pl=shop_1&vcode=76a0408bf648c47597fc6a5786aeae09"><img src="http://image.neverlands.ru/cities/forpost/loc5_c_n.png" onmouseover="this.src = 'http://image.neverlands.ru/cities/forpost/loc5_c_n_hl.png'; tooltip(this,'Ëàâêà');" onmouseout="this.src='http://image.neverlands.ru/cities/forpost/loc5_c_n.png'; hide_info(this);" /></a></div>
              <div style="position:absolute; left: 0; top: 25;"><a href="main.php?get_id=56&act=10&go=up&vcode=f4ee18b9c9ea2f483836729311db8d4c"><img src="http://image.neverlands.ru/cities/forpost/loc5_d_n.png" onmouseover="this.src = 'http://image.neverlands.ru/cities/forpost/loc5_d_n_hl.png'; tooltip(this,'Âûõîä èç ãîðîäà');" onmouseout="this.src='http://image.neverlands.ru/cities/forpost/loc5_d_n.png'; hide_info(this);" /></a></div>
              <div style="position:absolute; left: 982; top: 182;"><a href="main.php?get_id=56&act=10&go=build&pl=workshop&vcode=77aecc6067be86c8fb1690d213ffbdb6"><img src="http://image.neverlands.ru/cities/forpost/loc5_e_n.png" onmouseover="this.src = 'http://image.neverlands.ru/cities/forpost/loc5_e_n_hl.png'; tooltip(this,'Ìàñòåðñêàÿ');" onmouseout="this.src='http://image.neverlands.ru/cities/forpost/loc5_e_n.png'; hide_info(this);" /></a></div>
              <div style="position:absolute; left: 807; top: 282;"><a href="main.php?get_id=56&act=10&go=build&pl=hospi&vcode=1406e0a20e116475a65a79eaeb5c80e9"><img src="http://image.neverlands.ru/cities/forpost/loc5_f_n.png" onmouseover="this.src = 'http://image.neverlands.ru/cities/forpost/loc5_f_n_hl.png'; tooltip(this,'Áîëüíèöà');" onmouseout="this.src='http://image.neverlands.ru/cities/forpost/loc5_f_n.png'; hide_info(this);" /></a></div>
              <div style="position:absolute; left: 308; top: 512;"><a href="main.php?get_id=56&act=10&go=forpost3&vcode=b382080bd7db5d455bc5db2d8a7646d4"><img src="http://image.neverlands.ru/cities/forpost/loc5_arrow_1.png" onmouseover="this.src = 'http://image.neverlands.ru/cities/forpost/loc5_arrow_1_hl.png'; tooltip(this,'Ïåðåéòè â äåëîâîé êâàðòàë');" onmouseout="this.src='http://image.neverlands.ru/cities/forpost/loc5_arrow_1.png'; hide_info(this);" /></a></div>
              <div style="position:absolute; left: 900; top: 505;"><a href="main.php?get_id=56&act=10&go=forpost1&vcode=33764ca99b7a854057a4200e28cc7e2c"><img src="http://image.neverlands.ru/cities/forpost/loc5_arrow_2.png" onmouseover="this.src = 'http://image.neverlands.ru/cities/forpost/loc5_arrow_2_hl.png'; tooltip(this,'Ïåðåéòè â æèëîé êâàðòàë');" onmouseout="this.src='http://image.neverlands.ru/cities/forpost/loc5_arrow_2.png'; hide_info(this);" /></a></div></div>
              */
            var s1 = "tooltip(this,'" + area + "')";
            var p1 = html.IndexOf(s1, StringComparison.OrdinalIgnoreCase);
            
            if (p1 == -1)
            {
                return string.Empty;
            }

            var staticOnClick = @"href=""";
            var p2 = html.LastIndexOf(staticOnClick, p1, StringComparison.OrdinalIgnoreCase);
            if (p2 == -1)
            {
                return string.Empty;
            }

            var p3 = html.IndexOf('"', p2 + staticOnClick.Length);
            return p3 == -1 ? string.Empty : BuildRedirect("Навигация по городу", html.Substring(p2 + staticOnClick.Length, p3 - p2 - staticOnClick.Length));            
        }

        private static string MainPhpStartFromCityNavigation(string html)
        {
            var cityGates = new[] { "8-259", "8-294", "8-197", "12-428", "12-494", "12-521" };

            // /city1_fon_1.jpg width=760 height=255 border=0 USEMAP="#links">

            if (AppVars.AutoMovingMapPath == null ||
                AppVars.AutoMovingMapPath.Path == null ||
                AppVars.AutoMovingMapPath.Path.Length == 0 || 
                !AppVars.AutoMovingMapPath.CanUseExistingPath(AppVars.Profile.MapLocation, AppVars.AutoMovingDestinaton))
            {
                var dest = new[] { AppVars.AutoMovingDestinaton };
                AppVars.AutoMovingMapPath = new MapPath(AppVars.Profile.MapLocation, dest);
            }

            if (AppVars.AutoMovingMapPath.Path.Length < 2)
                return null;

            var gateLocation = AppVars.AutoMovingMapPath.Path[1];
            if (Array.IndexOf(cityGates, gateLocation) < 0)
            {
                gateLocation = AppVars.AutoMovingMapPath.Path[0];
                if (Array.IndexOf(cityGates, gateLocation) < 0)
                    return null;
            }

            const string pat2 = ".jpg width=760 height=255 border=0 USEMAP=\"#links\">";
            var pos2 = html.IndexOf(pat2, StringComparison.CurrentCultureIgnoreCase);
            if (pos2 == -1)
                return null;

            var pos1 = html.LastIndexOf("/", pos2, StringComparison.CurrentCultureIgnoreCase);
            if (pos1 == -1)
                return null;

            var cityPos = html.Substring(pos1 + 1, pos2 - pos1 - 1);
            if (string.IsNullOrEmpty(cityPos))
                return null;

            var links = new SortedDictionary<string, string>
            {
                {"city1_fon_1:8-259", "Выход из города"},
                {"city1_fon_1:8-294", "Перейти в жилой квартал"},
                {"city1_fon_2:8-259", "Перейти на городскую площадь"},
                {"city1_fon_2:8-294", "Перейти в квартал закона"},
                {"city1_fon_3:8-259", "Перейти в жилой квартал"},
                {"city1_fon_3:8-294", "Перейти в жилой квартал"},
                {"city1_fon_4:8-259", "Перейти на городскую площадь"},
                {"city1_fon_4:8-294", "Перейти на городскую площадь"},
                {"city1_fon_5_new:8-259", "Перейти в жилой квартал"},
                {"city1_fon_5_new:8-294", "Выход из города"},
                {"country1_fon:8-197", "Выход из деревни"},
                {"city2_1:12-428", "Выход из города, Западные Ворота"},
                {"city2_1:12-494", "Перейти в Торговый Квартал"},
                {"city2_1:12-521", "Перейти в Торговый Квартал"},
                {"city2_2_new:12-428", "Перейти на Центральную Площадь"},
                {"city2_2_new:12-494", "Перейти в Промышленный Квартал"},
                {"city2_2_new:12-521", "Перейти в Промышленный Квартал"},
                {"city2_3:12-428", "Перейти на Центральную Площадь"},
                {"city2_3:12-494", "Перейти в Промышленный Квартал"},
                {"city2_3:12-521", "Перейти в Промышленный Квартал"},
                {"city2_4:12-428", "Перейти в Торговый Квартал"},
                {"city2_4:12-494", "Перейти к Конюшням"},
                {"city2_4:12-521", "Перейти к Конюшням"},
                {"city2_5:12-428", "Перейти в Промышленный Квартал"},
                {"city2_5:12-494", "Перейти на Площадь Гильдий"},
                {"city2_5:12-521", "Перейти на Площадь Гильдий"},
                {"city2_6:12-428", "Перейти в жилой квартал"},
                {"city2_6:12-494", "Перейти к Конюшням"},
                {"city2_6:12-521", "Перейти к Конюшням"},
                {"city2_7_exit:12-428", "Перейти в Промышленный Квартал"},
                {"city2_7_exit:12-494", "Перейти на Площадь Гильдий"},
                {"city2_7_exit:12-521", "Выход из города, Южные Ворота"},
                {"city2_8:12-428", "Перейти в Деловой Квартал"},
                {"city2_8:12-494", "Выход из города, Восточные Ворота"},
                {"city2_8:12-521", "Перейти к Конюшням"},
                {"city2_8_elko:12-428", "Перейти в Деловой Квартал"},
                {"city2_8_elko:12-494", "Выход из города, Восточные Ворота"},
                {"city2_8_elko:12-521", "Перейти к Конюшням"},
                {"city2_9:12-428", "Перейти в Квартал Знаний"},
                {"city2_9:12-494", "Перейти в Квартал Знаний"},
                {"city2_9:12-521", "Перейти в Квартал Знаний"}
            };

            string textLink;
            if (!links.TryGetValue($"{cityPos}:{gateLocation}", out textLink))
                return null;

            var newhtml = MainPhpCityArea(html, textLink);
            if (string.IsNullOrEmpty(newhtml))
                return null;

            return newhtml;
        }
    }
}