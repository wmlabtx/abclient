namespace ABClient.PostFilter
{
    using System;
    using System.Globalization;
    using MyHelpers;

    internal static partial class Filter
    {
        private static string MainPhpFindFlora(string html)
        {
            if (html.IndexOf(@"<input type=button class=lbutdis value=""Причал"" DISABLED>", StringComparison.OrdinalIgnoreCase) != -1)
            {
                return string.Empty;
            }

            const string s1 = @"value=""" + "Вернуться" + @""">";
            var p1 = html.IndexOf(s1, StringComparison.OrdinalIgnoreCase);
            if (p1 == -1)
            {
                return string.Empty;
            }

            const string staticOnClick = @"onclick=""location='";
            var p2 = html.LastIndexOf(staticOnClick, p1, StringComparison.OrdinalIgnoreCase);
            if (p2 == -1)
            {
                return string.Empty;
            }

            var p3 = html.IndexOf(@"'", p2 + staticOnClick.Length, StringComparison.Ordinal);
            return p3 == -1 ? string.Empty : BuildRedirect("Переключение на природу", html.Substring(p2 + staticOnClick.Length, p3 - p2 - staticOnClick.Length));
        }

        private static string MainPhpFindInvOld(string html, string filter)
        {
            var tag = HelperStrings.SubString(
                html,
                @"<input type=button class=lbut value=""Инвентарь""",
                ">");
            if (!string.IsNullOrEmpty(tag))
            {
                if (tag.IndexOf("DISABLED", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    return null;
                }
            }

            const string s1 = @"value=""Инвентарь"">";
            var p1 = html.IndexOf(s1, StringComparison.OrdinalIgnoreCase);
            if (p1 != -1)
            {
                const string staticOnClick = @"onclick=""location='";
                var p2 = html.LastIndexOf(staticOnClick, p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                {
                    return string.Empty;
                }

                var p3 = html.IndexOf(@"'", p2 + staticOnClick.Length, StringComparison.Ordinal);
                return p3 == -1
                           ? string.Empty
                           : BuildRedirect("Переключение на инвентарь",
                                           html.Substring(p2 + staticOnClick.Length, p3 - p2 - staticOnClick.Length) + filter);
            }

            const string s1X = @"class=lbut value=""Инвентарь""";
            var p1X = html.IndexOf(s1X, StringComparison.OrdinalIgnoreCase);
            if (p1X != -1)
            {
                const string staticOnClick = @"onclick=""location='";
                var p2 = html.IndexOf(staticOnClick, p1X, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                {
                    return string.Empty;
                }

                var p3 = html.IndexOf(@"'", p2 + staticOnClick.Length, StringComparison.Ordinal);
                return p3 == -1
                           ? string.Empty
                           : BuildRedirect("Переключение на инвентарь",
                                           html.Substring(p2 + staticOnClick.Length, p3 - p2 - staticOnClick.Length) + filter);
            }

            return null;            
        }

        private static string MainPhpFindInv(string html, string filter)
        {
            /*
            var arpar = ["Черный",16,0,"none","","",2,"m_1002_996",0,0,0,545,"",1,0,3,"","n",1281952554,"","","0"];
            var inshp = [915,915,7,7,750,9000];
            var vcode = ["625b5901486a33d7934d4c148865af5b","75fec02648c91c48b54cfe8ed16cb8fe","ea23916df0264c0d75630487788b1d94","de0cdcde7d75158f74bc757a31663e2c","fe0ef96a6538cab87c1d3a4710e5b373",""];
            var crcount = [50,35,57,52,0,1,0,0,1,0];
            var data = [];
            view_arena();
             * 
             * main.php?get_id=56&act=10&go=inv&vcode='+vcode[1]
             */

            if (html.IndexOf("view_arena();", StringComparison.OrdinalIgnoreCase) != -1)
            {
                const string patternArena = @"var vcode = [";
                var posPatternArena = html.IndexOf(patternArena, StringComparison.OrdinalIgnoreCase);
                if (posPatternArena == -1)
                {
                    return null;
                }

                posPatternArena += patternArena.Length;
                var posArenaEnd = html.IndexOf(']', posPatternArena);
                if (posArenaEnd == -1)
                {
                    return null;
                }

                var vcodeargs = html.Substring(posPatternArena, posArenaEnd - posPatternArena);
                var pvcode = vcodeargs.Split(',');
                if (pvcode.Length < 1)
                {
                    return null;
                }

                var avcode = pvcode[1].Trim(new[] { '"' });
                if (string.IsNullOrEmpty(avcode))
                {
                    return null;
                }

                var alink = string.Format(CultureInfo.InvariantCulture, "main.php?get_id=56&act=10&go=inv&vcode={0}{1}", avcode, filter);
                return BuildRedirect("Переключение на инвентарь", alink);
            }

            /*
             * (vcode[1][0] ? ' <input type=button class=fr_but value="Инвентарь" '+(vcode[1][1]!='' ? 'onclick="location=\'main.php?get_id=56&act=10&go=inv&vcode='+vcode[1][1]+'\'"' : 'DISABLED')+'> ' : '')
             * var vcode = [[1,"a4a67ab9e59eb6da369f2fec5d730f81"],[1,"c7c7260eae7908873ba54456422f5000"],[1,"4078e9303ec2ba66bf34a100f06feb45"]];
             * view_taverna();
             * main.php?get_id=56&act=10&go=inv&vcode=88f557c7220c8ed014c9dcbfb5f04e5c
             * 
             * var vcode = [[1,"a40800e502f894fad9c736e8277d91a7"],[1,"6dd55de887f3ade249bdbecfc5a64cfe"],[1,"c987513b6f0c99c6d053320a2b170a3c"]];
             * 
             */

            if (
                (html.IndexOf("view_moor()", StringComparison.OrdinalIgnoreCase) != -1) ||
                (html.IndexOf("view_taverna()", StringComparison.OrdinalIgnoreCase) != -1) ||
                (html.IndexOf("view_magic_sch()", StringComparison.OrdinalIgnoreCase) != -1) ||
                (html.IndexOf("view_library()", StringComparison.OrdinalIgnoreCase) != -1) ||
                (html.IndexOf("view_teleport()", StringComparison.OrdinalIgnoreCase) != -1)
               )
            {
                var patternArena = @"var vcode = [";
                var posPatternArena = html.IndexOf(patternArena, StringComparison.OrdinalIgnoreCase);
                if (posPatternArena == -1)
                {
                    return null;
                }

                posPatternArena += patternArena.Length;
                patternArena = @",[1,""";
                posPatternArena = html.IndexOf(patternArena, posPatternArena, StringComparison.OrdinalIgnoreCase);
                if (posPatternArena == -1)
                {
                    return null;
                }

                posPatternArena += patternArena.Length;
                var posArenaEnd = html.IndexOf("]", posPatternArena, StringComparison.Ordinal);
                if (posArenaEnd == -1)
                {
                    return null;
                }

                var avcode = html.Substring(posPatternArena, posArenaEnd - posPatternArena - 1); 
                if (string.IsNullOrEmpty(avcode))
                {
                    return null;
                }

                var alink = string.Format(CultureInfo.InvariantCulture, "main.php?get_id=56&act=10&go=inv&vcode={0}{1}", avcode, filter);
                return BuildRedirect("Переключение на инвентарь", alink);
            }

            if (html.IndexOf("Инвентарь", StringComparison.OrdinalIgnoreCase) == -1)
            {
                if (html.IndexOf("<input type=button class=lbut onclick=\"location='../main.php'\" value=\"Вернуться\">", StringComparison.Ordinal) != -1)
                    return BuildRedirect("Переключение на инвентарь", "main.php");

                return null;
            }

            // <input type=button class=lbut value="Инвентарь" onclick="location='main.php?get_id=56&act=10&go=inv&vcode=6770ad30c2fdf8b6c6d3d1c767186f2b'">


            var newHtml = MainPhpFindInvOld(html, filter);
            if (!string.IsNullOrEmpty(newHtml))
            {
                return newHtml;
            }

            /* ["inv","Инвентарь","b4e6bd1a57c0504df6734294cfee6d52", */

            const string patternEnter = @"[""inv"",""Инвентарь"",""";
            var posPatternEnter = html.IndexOf(patternEnter, StringComparison.OrdinalIgnoreCase);
            if (posPatternEnter == -1)
            {
                return null;
            }

            posPatternEnter += patternEnter.Length;
            var posEnd = html.IndexOf('"', posPatternEnter);
            if (posEnd == -1)
            {
                return null;
            }

            var vcode = html.Substring(posPatternEnter, posEnd - posPatternEnter);
            var link = string.Format(CultureInfo.InvariantCulture, "main.php?get_id=56&act=10&go=inv&vcode={0}{1}", vcode, filter);
            return BuildRedirect("Переключение на инвентарь", link);
        }

        private static bool MainPhpIsInv(string html)
        {
            return html.IndexOf(@"<a href=""?im=0""><img", StringComparison.OrdinalIgnoreCase) != -1;
        }

        /*
         * 
        *   <a href="?im=0"><img src=http://image.neverlands.ru/gameplay/invent/0.gif width=44 height=53 alt="Вещи" title="Вещи" class=cath border=0></a>
        *   ...
        *   <a href="?wfo=1"><img src=http://image.neverlands.ru/gameplay/invent/cat/b3.gif width=41 height=53 alt="Сбросить фильтр" title="Сбросить фильтр" class=cath border=0></a>          * 
         */

        private static string MainPhpFindPerc(string html)
        {
            /* ["inf","Ваш персонаж","d99fc020c89db0c07e41fbd972179422", */
            /* <input type=button class=lbut onclick="location='main.php?get_id=56&act=10&go=inf&vcode=86d216564bc981c83a54f59197d3655a'" value="Ваш персонаж"> */

            /*
            var delta = DateTime.Now.Subtract(AppVars.LastSwitch);
            if (delta.TotalSeconds < 5)
            {
                return null;
            }
             */


            var vcode = HelperStrings.SubString(html, "'main.php?get_id=56&act=10&go=inf&vcode=", "'");
            string link;
            if (!string.IsNullOrEmpty(vcode))
            {
                link = string.Format(CultureInfo.InvariantCulture, "main.php?get_id=56&act=10&go=inf&vcode={0}", vcode);
                return BuildRedirect("Переключение на персонаж", link);                
            }

            const string patternEnter = @"[""inf"",""Ваш персонаж"",""";
            var posPatternEnter = html.IndexOf(patternEnter, StringComparison.OrdinalIgnoreCase);
            if (posPatternEnter == -1)
                return null;

            posPatternEnter += patternEnter.Length;
            var posEnd = html.IndexOf('"', posPatternEnter);
            if (posEnd == -1)
            {
                return null;
            }

            AppVars.LastSwitch = DateTime.Now;
            vcode = html.Substring(posPatternEnter, posEnd - posPatternEnter);
            link = string.Format(CultureInfo.InvariantCulture, "main.php?get_id=56&act=10&go=inf&vcode={0}", vcode);
            return BuildRedirect("Переключение на персонаж", link);
        }

        private static bool MainPhpIsPerc(string html)
        {
            return html.IndexOf(@"input type=button class=lbut value=""Умения""", StringComparison.OrdinalIgnoreCase) != -1;
        }

        private static string MainPhpFindEnter(string html)
        {
            /* []],["dep","Войти","81446abde14e36b0be813c60b2990f43",[]]]; */
            
            const string patternEnter = @"[""dep"",""Войти"",""";
            var posPatternEnter = html.IndexOf(patternEnter, StringComparison.OrdinalIgnoreCase);
            if (posPatternEnter == -1)
            {
                return null;
            }

            posPatternEnter += patternEnter.Length;
            var posEnd = html.IndexOf('"', posPatternEnter);
            if (posEnd == -1)
            {
                return null;
            }

            var vcode = html.Substring(posPatternEnter, posEnd - posPatternEnter);
            var link = string.Format(CultureInfo.InvariantCulture, "main.php?get_id=56&act=10&go=dep&vcode={0}", vcode);
            return BuildRedirect("Вход", link);
        }

        private static string MainPhpFindDrink(string html)
        {
            /* ["dri","Пить","617caee9766e7ff83e24e1ac4753bc10",[]]]; */

            const string patternDrink = @"[""dri"",""Пить"",""";
            var posPatternDrink = html.IndexOf(patternDrink, StringComparison.OrdinalIgnoreCase);
            if (posPatternDrink == -1)
            {
                return null;
            }

            posPatternDrink += patternDrink.Length;
            var posEnd = html.IndexOf('"', posPatternDrink);
            if (posEnd == -1)
            {
                return null;
            }

            var vcode = html.Substring(posPatternDrink, posEnd - posPatternDrink);
            var callDrink = string.Format(CultureInfo.InvariantCulture, "Drink('{0}');", vcode);
            const string patternViewMap = "view_map();";
            var poscript = html.IndexOf(patternViewMap, StringComparison.OrdinalIgnoreCase);
            if (poscript != -1)
            {
                poscript += patternViewMap.Length;
                html = html.Insert(poscript, callDrink);
            }

            return html;
        }

        private static string MainPhpFindFish(string html)
        {
            /* "fis","Рыбалка","65d68e7ff617263578c4f943a917aae9" */

            const string patternDrink = @"[""fis"",""Рыбалка"",""";
            var posPatternDrink = html.IndexOf(patternDrink, StringComparison.OrdinalIgnoreCase);
            if (posPatternDrink == -1)
            {
                return null;
            }

            posPatternDrink += patternDrink.Length;
            var posEnd = html.IndexOf('"', posPatternDrink);
            if (posEnd == -1)
            {
                return null;
            }

            var vcode = html.Substring(posPatternDrink, posEnd - posPatternDrink);
            var callDrink = string.Format(CultureInfo.InvariantCulture, "Fish('{0}');", vcode);
            const string patternViewMap = "view_map();";

            var poscript = html.IndexOf(patternViewMap, StringComparison.OrdinalIgnoreCase);
            if (poscript != -1)
            {
                poscript += patternViewMap.Length;
                html = html.Insert(poscript, callDrink);
            }

            return html;

            /*
            var s1 = @"value=""" + "Рыбалка" + @"""";
            var p1 = html.IndexOf(s1, StringComparison.OrdinalIgnoreCase);
            if (p1 == -1)
            {
                return string.Empty;
            }

            var staticOnClick = @"onclick=""location='";
            var p2 = html.IndexOf(staticOnClick, p1 + s1.Length, StringComparison.OrdinalIgnoreCase);
            if (p2 == -1)
            {
                return string.Empty;
            }

            p2 += staticOnClick.Length;
            var p3 = html.IndexOf(@"'", p2);
            return p3 == -1 ? string.Empty : BuildRedirect("Переключение на рыбалку", html.Substring(p2, p3 - p2));
             */ 
        }

        private static bool MainPhpIsA(string html)
        {
            return html.IndexOf(@"<a href=?useaction=addon-action&addid=2>", StringComparison.OrdinalIgnoreCase) != -1;
        }

        /*
        private static void MainPhpCheckOrl(string html)
        {
            // ["ogl","Оглядеться","e108c4be017325666d1744fd977189f4", 

            var patternEnter = @"[""ogl"",""Оглядеться"",""";
            var posPatternEnter = html.IndexOf(patternEnter, StringComparison.OrdinalIgnoreCase);
            if (posPatternEnter == -1)
            {
                return;
            }

            posPatternEnter += patternEnter.Length;
            var posEnd = html.IndexOf('"', posPatternEnter);
            if (posEnd == -1)
            {
                return;
            }

            var vcode = html.Substring(posPatternEnter, posEnd - posPatternEnter);
            var link = string.Format(CultureInfo.InvariantCulture, "http://www.neverlands.ru/gameplay/ajax/alchemy_ajax.php?act=1&vcode={0}&r=0", vcode);
            AppVars.LastTied = DateTime.Now;
            var wc = new WebClient { Proxy = AppVars.LocalProxy };
            wc.DownloadData(new Uri(link));
        }
         */ 
    }
}