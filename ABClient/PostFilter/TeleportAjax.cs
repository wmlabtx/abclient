using ABClient.ABForms;
using System;
using System.Globalization;
using ABClient.ExtMap;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static string TeleportAjax(string html)
        {
            if (AppVars.AutoMovingMapPath == null || !AppVars.AutoMovingMapPath.CanUseExistingPath(AppVars.Profile.MapLocation, AppVars.AutoMovingDestinaton))
            {
                var dest = new[] { AppVars.AutoMovingDestinaton };
                AppVars.AutoMovingMapPath = new MapPath(AppVars.Profile.MapLocation, dest);
            }

            AppVars.AutoMovingNextJump = AppVars.AutoMovingMapPath.NextJump;
            AppVars.AutoMovingJumps = AppVars.AutoMovingMapPath.Jumps;
            AppVars.AutoMovingCityGate = AppVars.AutoMovingMapPath.CityGate;

            if (AppVars.AutoMovingJumps == 0)
            {
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new NavigatorOffInvokeDelegate(AppVars.MainForm.NavigatorOffInvoke),
                            new object[] { });
                    }
                }
                catch (InvalidOperationException)
                {
                }

                return null;
            }

            var telep = MyHelpers.HelperStrings.SubString(html, "var telep = [[", "]];");
            if (string.IsNullOrEmpty(telep))
                return null;

            var stelep = telep.Split(new[] { "],[" }, StringSplitOptions.None);
            foreach (var etelep in stelep)
            {
                var pars = etelep.Split(',');
                int x, y;
                if (!int.TryParse(pars[0], out x))
                    continue;

                if (!int.TryParse(pars[1], out y))
                    continue;

                var coor = Map.MakePosition(x, y);
                if (!Map.Location.ContainsKey(coor))
                    continue;

                var regnum = Map.Location[coor].RegNum;
                if (regnum == null)
                    continue;

                if (!regnum.Equals(AppVars.AutoMovingNextJump))
                    continue;

                var pr = pars[3];
                var vcode = pars[4].Trim('"');
                var link = string.Format(CultureInfo.InvariantCulture, "main.php?get_id=16&act=1&x={0}&y={1}&pr={2}&vcode={3}", x, y, pr, vcode);
                html = BuildRedirect($"Телепорт {pars[2]}", link);
                return html;
            }

            // 'main.php?get_id=56&act=10&go=up&vcode='+vcode[2][1]+'\'"'
            var build = MyHelpers.HelperStrings.SubString(html, "var vcode = [[", "]];");
            if (string.IsNullOrEmpty(build))
                return null;

            var sbuild = build.Split(new[] { "],["}, StringSplitOptions.None);
            if (sbuild.Length >= 3)
            {
                var pars = sbuild[2].Split(',');
                if (pars.Length >= 2)
                {
                    var vcodex = pars[1].Trim('\"');
                    var linkx = $"main.php?get_id=56&act=10&go=up&vcode={vcodex}";
                    html = BuildRedirect("Выходим из телепорта", linkx);
                    return html;
                }
            }

            return null;
        }
    }
}
 