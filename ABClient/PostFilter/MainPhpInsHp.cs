using System.Globalization;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static void MainPhpInsHp(string html, int inshp)
        {
            var epos = html.IndexOf(')', inshp);
            if (epos == -1)
            {
                return;
            }

            var spar = html.Substring(inshp, epos - inshp);
            var par = spar.Split(',');
            if (par.Length != 6)
            {
                return;
            }

            double hp;
            if (double.TryParse(par[4], NumberStyles.Any, CultureInfo.InvariantCulture, out hp))
            {
                AppVars.Profile.Pers.IntHP = hp;
            }

            double ma;
            if (double.TryParse(par[5], NumberStyles.Any, CultureInfo.InvariantCulture, out ma))
            {
                AppVars.Profile.Pers.IntMA = ma;
            }
        }
    }
}