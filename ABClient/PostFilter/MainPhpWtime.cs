namespace ABClient.PostFilter
{
    using System;
    using System.Globalization;
    using ABForms;
    using ExtMap;

    internal static partial class Filter
    {
        private static string MainPhpWtime(string address, string html)
        {
            if (AppVars.Profile.FishAuto)
            {
                var newhtml = MainPhpFishReport(html);
                if (!string.IsNullOrEmpty(newhtml))
                {
                    html = newhtml;
                    goto end;
                }
            }

            html = html.Replace("id=wtime></div>", "id=wtime><i>Выполняется действие...</i></div>");
            /*
            var staticWtime = @" id=wtime>";
            var staticWtimeEnd = @"</div>";
             */
            var staticScriptEnd = "</SCRIPT>";
            var poswt = html.LastIndexOf(staticScriptEnd, html.Length - 1, StringComparison.OrdinalIgnoreCase);
            if (poswt != -1)
            {
                poswt += staticScriptEnd.Length;
            }
            
            /*
            var poswt = html.IndexOf(staticWtime, StringComparison.OrdinalIgnoreCase);
            if (poswt != -1)
            {
                poswt = html.IndexOf(staticWtimeEnd, poswt, StringComparison.OrdinalIgnoreCase);
                if (poswt != -1)
                {
                    poswt += staticWtimeEnd.Length;
                }
            }
             */ 

            if (AppVars.Profile.MapShowExtend && Map.InvLocation.ContainsKey(AppVars.Profile.MapLocation))
            {
                var gx = 0;
                var gy = 0;
                var p1 = address.IndexOf("&gx=", StringComparison.OrdinalIgnoreCase);
                if (p1 != -1)
                {
                    p1 += "&gx=".Length;
                    var p2 = address.IndexOf('&', p1);
                    if (p2 != -1)
                    {
                        var sgx = address.Substring(p1, p2 - p1);
                        if (int.TryParse(sgx, out gx))
                        {
                            p2 += "&gx=".Length;
                            var p3 = address.IndexOf('&', p2 + 1);
                            if (p3 != -1)
                            {
                                var sgy = address.Substring(p2, p3 - p2);
                                if (int.TryParse(sgy, out gy))
                                {
                                }
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(AppVars.Profile.MapLocation))
                {
                    var maphtml = Map.ShowMiniMap(gx, gy);
                    if (poswt != -1)
                    {
                        html = html.Insert(poswt, maphtml);
                    }

                    /*
                    var pos = html.IndexOf("counterview(", StringComparison.OrdinalIgnoreCase);
                    if (pos != -1)
                    {
                        pos = html.LastIndexOf("<SCRIPT", pos, StringComparison.OrdinalIgnoreCase);
                        if (pos != -1)
                        {
                            html = html.Insert(pos, maphtml);
                        }
                    }
                     */ 
                }
            }

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateCheckTiedDelegate(FormMain.UpdateCheckTied), new object[] { });
                }
            }
            catch (InvalidOperationException)
            {
            }

            if (poswt != -1)
            {
                if (AppVars.AutoMoving && AppVars.AutoMovingJumps > 0)
                {
                    html = html.Insert(
                        poswt,
                        string.Format(
                            CultureInfo.InvariantCulture,
                            @"<font class=nickname><div align=center style=""color: #660066;""><i>Пункт назначения: <b>{0}</b><br>Еще переходов: <b>{1}</b></i></div></font>",
                            AppVars.AutoMovingDestinaton,
                            AppVars.AutoMovingJumps));
                    goto end;
                }

                if (AppVars.AutoDrink || AppVars.AutoFishDrink || AppVars.AutoFishDrinkOnce)
                {
                    html = html.Insert(
                        poswt,
                        @"<font class=nickname><div align=center style=""color: #006600;""><i>Работает автопитье</i></div></font>");
                    AppVars.AutoFishDrinkOnce = false;
                }
            }

            end:
            return html;
        }
    }
}